import React from 'react';
import PropTypes from 'prop-types';

import { connect } from 'react-redux';

import { PageHeader, Well, Alert, Row, Col } from 'react-bootstrap';
import { ButtonToolbar, Button, ButtonGroup, Glyphicon } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import _ from 'lodash';
import Moment from 'moment';
import { formatDateTime, toZuluTime, today } from '../utils/date';
import Promise from 'bluebird';

import * as Action from '../actionTypes';
import * as Api from '../api';
import * as Constant from '../constants';
import store from '../store';

import CheckboxControl from '../components/CheckboxControl.jsx';
import DateControl from '../components/DateControl.jsx';
import Favourites from '../components/Favourites.jsx';
import KeySearchControl from '../components/KeySearchControl.jsx';
import MultiDropdown from '../components/MultiDropdown.jsx';
import SortTable from '../components/SortTable.jsx';
import Markdown from 'react-markdown';
import Spinner from '../components/Spinner.jsx';
import Tooltips from '../components/Tooltips.jsx';
import Authorize from '../components/Authorize';
import UpdateButton from '../components/UpdateButton.jsx';
import ExportButton from '../components/ExportButton.jsx';
import FilterDropdown from '../components/FilterDropdown.jsx';

class CCWNotifications extends React.Component {
  static propTypes = {
    currentUser: PropTypes.object,
    districts: PropTypes.object,
    inspectors: PropTypes.object,
    ccwnotifications: PropTypes.object,
    favourites: PropTypes.object,
    search: PropTypes.object,
    ui: PropTypes.object,
    router: PropTypes.object,
    owners: PropTypes.object,
  };

  constructor(props) {
    super(props);
    var defaultSelectedInspectors = props.currentUser.isInspector ? [props.currentUser.id] : [];

    var defaultSelectedDistricts =
      props.currentUser.isInspector || !props.currentUser.districtId ? [] : [props.currentUser.districtId];

    var defaultDateFrom = toZuluTime(new Date('2020-09-01'));
    var defaultDateTo = today();

    this.state = {
      loading: true,
      selectAll: false,
      searched: false,

      search: {
        selectedDistrictsIds: props.search.selectedDistrictsIds || defaultSelectedDistricts,
        selectedInspectorsIds: props.search.selectedInspectorsIds || defaultSelectedInspectors,
        keySearchField: props.search.keySearchField,
        keySearchText: props.search.keySearchText,
        keySearchParams: props.search.keySearchParams,
        hideRead: props.search.hideRead || true,
        dateFrom: props.search.dateFrom || defaultDateFrom,
        dateTo: props.search.dateTo || defaultDateTo,
        loaded: props.search.loaded || true,
        ownerId: props.search.ownerId || 0,
        ownerName: props.search.ownerName || 'Owner',
      },

      ui: {
        sortField: props.ui.sortField || 'dateDetected',
        sortDesc: true,
      },
    };
  }

  buildSearchParams = () => {
    var searchParams = {
      hideRead: this.state.search.hideRead,
    };

    var dateFrom = Moment(this.state.search.dateFrom);
    if (dateFrom && dateFrom.isValid()) {
      searchParams.dateFrom = toZuluTime(dateFrom.startOf('day'));
    }

    var dateTo = Moment(this.state.search.dateTo);
    if (dateTo && dateTo.isValid()) {
      searchParams.dateTo = toZuluTime(dateTo.startOf('day'));
    }

    if (this.state.search.keySearchParams) {
      // Use key search control params if there
      return { ...this.state.search.keySearchParams, ...searchParams };
    }

    if (
      this.state.search.selectedDistrictsIds.length > 0 &&
      this.state.search.selectedDistrictsIds.length !== _.size(this.props.districts)
    ) {
      searchParams.districts = this.state.search.selectedDistrictsIds;
    }

    if (
      this.state.search.selectedInspectorsIds.length > 0 &&
      this.state.search.selectedInspectorsIds.length !== _.size(this.props.inspectors)
    ) {
      searchParams.inspectors = this.state.search.selectedInspectorsIds;
    }

    searchParams.owner = this.state.search.ownerId || '';

    return searchParams;
  };

  componentDidMount() {
    this.setState({ loading: true });

    var inspectorsPromise = Api.getInspectors();
    var favouritesPromise = Api.getFavourites('ccwnotifications');
    var ownersPromise = Api.getOwners();

    Promise.all([inspectorsPromise, favouritesPromise, ownersPromise])
      .then(() => {
        if (this.props.location.search) {
          // Check for specific school bus query
          var state = {
            selectedDistrictsIds: [],
            selectedInspectorsIds: this.props.currentUser.isInspector ? [this.props.currentUser.id] : [],
            keySearchField: this.props.search.keySearchField,
            keySearchText: '',
            keySearchParams: null,
            startDate: '',
            endDate: '',
            hideRead: true,
            selectAll: false,
            ownerId: 0,
            ownerName: 'Owner',
          };

          if (this.props.location.query[Constant.CCWNOTIRICATION_INSPECTORS_QUERY]) {
            state.selectedInspectorsIds = this.props.currentUser.isInspector ? [this.props.currentUser.id] : [];
          }

          this.updateSearchState(state, this.initialFetch);
        } else {
          if (!this.props.search.loaded) {
            // This is the first load so look for a default favourite
            var favourite = _.find(this.props.favourites, (favourite) => {
              return favourite.isDefault;
            });
            if (favourite) {
              this.loadFavourite(favourite);
            }
          }
          return this.initialFetch();
        }
      })
      .finally(() => {
        this.setState({ loading: false });
      });
  }

  initialFetch = () => {
    if (!this.props.currentUser.isInspector) return { loading: false };
    return this.fetch();
  };

  fetch = () => {
    this.setState({ loading: true });
    return Api.getCCWNotifications(this.buildSearchParams()).finally(() => {
      this.setState({ loading: false, searched: true });
    });
  };

  updateSearchState = (state, callback) => {
    this.setState({ search: { ...this.state.search, ...state, ...{ loaded: true } } }, () => {
      store.dispatch({
        type: Action.UPDATE_CCWNOTIFICATIONS_SEARCH,
        ccwnotifications: this.state.search,
      });
      if (callback) {
        callback();
      }
    });
  };

  updateUIState = (state, callback) => {
    this.setState({ ui: { ...this.state.ui, ...state } }, () => {
      store.dispatch({ type: Action.UPDATE_CCWNOTIFICATION_UI, ccwnotifications: this.state.ui });
      if (callback) {
        callback();
      }
    });
  };

  loadFavourite = (favourite) => {
    this.updateSearchState(JSON.parse(favourite.value), this.fetch);
  };

  updateCCWNotifications = (ccwnotifications, read) => {
    const notifications = _.pickBy(
      ccwnotifications,
      (ccwnotification) =>
        (this.props.currentUser.isSystemAdmin || ccwnotification.inspectorId === this.props.currentUser.id) &&
        ccwnotification.selected
    );

    const notificationArray = Object.values(notifications);

    if (notificationArray.length === 0) return;

    if (read) {
      Api.updateHasBeenReadAsRead(notificationArray).then(() => {
        this.fetch();
      });
    } else {
      Api.updateHasBeenReadAsUnread(notificationArray).then(() => {
        this.fetch();
      });
    }
  };

  deleteCCWNotifications = (ccwnotifications) => {
    const notifications = _.pickBy(
      ccwnotifications,
      (ccwnotification) =>
        (this.props.currentUser.isSystemAdmin || ccwnotification.inspectorId === this.props.currentUser.id) &&
        ccwnotification.selected
    );

    const notificationArray = Object.values(notifications);

    if (notificationArray.length === 0) return;

    Api.deleteCCWNotifications(notificationArray).then(() => {
      this.fetch();
    });
  };

  selectAll = (toggle) => {
    var ccwnotifications = { ...this.props.ccwnotifications };

    _.values(ccwnotifications).forEach((ccwnotification) => {
      if (this.props.currentUser.isSystemAdmin || ccwnotification.inspectorId === this.props.currentUser.id) {
        ccwnotification.selected = toggle.selectAll;
      }
    });
    store.dispatch({ type: Action.UPDATE_CCWNOTIFICATIONS, ccwnotifications: ccwnotifications });
  };

  togleSelected = (toggle, ccwnotification) => {
    var notification = { ...ccwnotification };
    notification.selected = toggle.selected;

    var ccwnotifications = { ...this.props.ccwnotifications, [notification.id]: notification };

    store.dispatch({ type: Action.UPDATE_CCWNOTIFICATIONS, ccwnotifications: ccwnotifications });
  };

  getExportData = () => {
    const ccwnotifications = _.sortBy(this.props.ccwnotifications, this.state.ui.sortField);
    if (this.state.ui.sortDesc) {
      _.reverse(ccwnotifications);
    }
    const notificationArray = _.values(ccwnotifications);

    const header = 'Date Detected, School Bus Reg #, Current Owner, Summary, Read?\n';

    const rows = notificationArray.map((x) => {
      const summary = x.ccwNotificationDetails
        .map((detail) => `${detail.colDescription} - New: ${detail.valueTo} Old: ${detail.valueFrom}`)
        .join('\n');

      return `"${formatDateTime(x.dateDetected, Constant.DATE_SHORT_MONTH_DAY_YEAR)}","${x.schoolBusRegNum ?? ''}","${
        x.schoolBusOwnerName ?? ''
      }","${summary}","${x.hasBeenViewed ? 'Y' : 'N'}"\n`;
    });

    return [header, ...rows];
  };

  render() {
    var districts = _.sortBy(this.props.districts, 'name');
    var inspectors = _.sortBy(this.props.inspectors, 'name');
    var owners = _.sortBy(this.props.owners, 'name');

    var ccwnotificationArray = _.values(this.props.ccwnotifications);
    var selectedNofications = ccwnotificationArray.filter((x) => x.selected);
    var anySelected = selectedNofications.length > 0;
    var readSelected = selectedNofications.filter((x) => x.hasBeenViewed).length > 0;
    var unreadSelected = selectedNofications.filter((x) => !x.hasBeenViewed).length > 0;

    var isEditable =
      this.props.currentUser.isSystemAdmin ||
      ccwnotificationArray.filter((x) => x.inspectorId === this.props.currentUser.id).length > 0;

    var numCCWNotifications = this.state.loading ? '...' : ccwnotificationArray.length;

    return (
      <div id="ccwnotifications">
        <PageHeader>
          ICBC Notifications ({numCCWNotifications})
          <ButtonGroup id="header-button-group">
            <ExportButton
              disabled={ccwnotificationArray.length === 0}
              getExportData={this.getExportData}
              filename="icbcnotification"
            />
          </ButtonGroup>
        </PageHeader>
        <div>
          <Well bsSize="small" className="search-bar clearfix">
            <Row>
              <Col xs={9} sm={10} id="filters">
                <Row>
                  <ButtonToolbar>
                    <MultiDropdown
                      id="selectedDistrictsIds"
                      placeholder="Districts"
                      items={districts}
                      selectedIds={this.state.search.selectedDistrictsIds}
                      updateState={this.updateSearchState}
                      showMaxItems={2}
                    />
                    <MultiDropdown
                      id="selectedInspectorsIds"
                      placeholder="Inspectors"
                      items={inspectors}
                      selectedIds={this.state.search.selectedInspectorsIds}
                      updateState={this.updateSearchState}
                      showMaxItems={2}
                    />
                    <DateControl
                      id="dateFrom"
                      date={this.state.search.dateFrom}
                      updateState={this.updateSearchState}
                      label="From:"
                      title="From"
                    />
                    <DateControl
                      id="dateTo"
                      date={this.state.search.dateTo}
                      updateState={this.updateSearchState}
                      label="To:"
                      title="To"
                    />
                    <Tooltips
                      placement="top"
                      text="The date range filters notifications based on the day these changes were detected, it may not be the same as when these changes occurred."
                    >
                      <Glyphicon glyph="question-sign" style={{ marginLeft: '5px', marginTop: '5px' }} />
                    </Tooltips>
                    <CheckboxControl
                      inline
                      id="hideRead"
                      checked={this.state.search.hideRead}
                      updateState={this.updateSearchState}
                      style={{ marginLeft: '20px', marginTop: '12px' }}
                    >
                      Hide Read
                    </CheckboxControl>
                  </ButtonToolbar>
                </Row>
                <Row>
                  <ButtonToolbar>
                    <FilterDropdown
                      id="ownerId"
                      placeholder="Owner"
                      blankLine="(All)"
                      items={owners}
                      selectedId={this.state.search.ownerId}
                      updateState={this.updateSearchState}
                    />
                    <KeySearchControl
                      id="ccwnotifications-key-search"
                      search={this.state.search}
                      updateState={this.updateSearchState}
                    />
                  </ButtonToolbar>
                </Row>
              </Col>
              <Col xs={3} sm={2} id="search-buttons">
                <Row>
                  <Favourites
                    id="ccwnotifications-faves-dropdown"
                    type="ccwnotifications"
                    favourites={this.props.favourites}
                    data={this.state.search}
                    onSelect={this.loadFavourite}
                    className="pull-right"
                  />
                </Row>
                <Row>
                  <Button id="search-button" className="pull-right" bsStyle="primary" onClick={this.fetch}>
                    Search
                  </Button>
                </Row>
              </Col>
            </Row>
          </Well>

          {(() => {
            if (this.state.loading) {
              return (
                <div style={{ textAlign: 'center' }}>
                  <Spinner />
                </div>
              );
            }

            var togleSelectAllButton = (
              <Authorize permissions={Constant.PERMISSION_SB_W}>
                <CheckboxControl
                  inline
                  id="selectAll"
                  checked={this.state.selectAlls}
                  disabled={!isEditable}
                  updateState={this.selectAll}
                >
                  &nbsp;
                </CheckboxControl>
              </Authorize>
            );

            if (Object.keys(this.props.ccwnotifications).length === 0) {
              if (this.state.searched) {
                return <Alert bsStyle="success">No ICBC notifications</Alert>;
              } else {
                return <Alert bsStyle="success">Click search button to retrieve ICBC notifications</Alert>;
              }
            }

            var ccwnotifications = _.sortBy(this.props.ccwnotifications, this.state.ui.sortField);
            if (this.state.ui.sortDesc) {
              _.reverse(ccwnotifications);
            }

            return (
              <React.Fragment>
                <Authorize permissions={Constant.PERMISSION_OWNER_W}>
                  <Row className="float-right">
                    <ButtonGroup>
                      <UpdateButton
                        enabledTooltip="Mark selected as Read"
                        disabledTooltip="Select unread notifications first"
                        hide={false}
                        disabled={!unreadSelected}
                        onConfirm={this.updateCCWNotifications.bind(this, this.props.ccwnotifications, true)}
                      >
                        <FontAwesomeIcon icon="envelope-open" />
                      </UpdateButton>
                      <UpdateButton
                        enabledTooltip="Mark selected as Unread"
                        disabledTooltip="Select read notifications first"
                        hide={false}
                        disabled={!readSelected}
                        onConfirm={this.updateCCWNotifications.bind(this, this.props.ccwnotifications, false)}
                      >
                        <FontAwesomeIcon icon="envelope" />
                      </UpdateButton>
                      <UpdateButton
                        enabledTooltip="Delete selected"
                        disabledTooltip="Select notifications first"
                        hide={false}
                        disabled={!anySelected}
                        onConfirm={this.deleteCCWNotifications.bind(this, this.props.ccwnotifications)}
                      >
                        <FontAwesomeIcon icon="trash-alt" />
                      </UpdateButton>
                    </ButtonGroup>
                  </Row>
                </Authorize>
                <SortTable
                  sortField={this.state.ui.sortField}
                  sortDesc={this.state.ui.sortDesc}
                  onSort={this.updateUIState}
                  headers={[
                    { field: 'dateDetected', title: 'Date Detected' },
                    { field: 'schoolBusRegNum', title: 'School Bus Reg#' },
                    { field: 'schoolBusOwnerName', title: 'Current Owner' },
                    { field: 'summary', title: 'Summary' },
                    {
                      field: 'togleHasBeenViewed',
                      title: 'Mark as Read/Unread',
                      style: { textAlign: 'right' },
                      node: togleSelectAllButton,
                    },
                  ]}
                >
                  {_.map(ccwnotifications, (ccwnotification) => {
                    return (
                      <tr key={ccwnotification.id} className={ccwnotification.hasBeenViewed ? 'info' : null}>
                        <td>{formatDateTime(ccwnotification.dateDetected, Constant.DATE_SHORT_MONTH_DAY_YEAR)}</td>
                        <td>
                          <a href={ccwnotification.schoolBusUrl}>{ccwnotification.schoolBusRegNum}</a>
                        </td>
                        <td>
                          <a href={ccwnotification.ownerURL}>{ccwnotification.schoolBusOwnerName}</a>
                        </td>
                        <td>
                          <Markdown source={ccwnotification.text}></Markdown>
                        </td>
                        <td>
                          <CheckboxControl
                            className="float-right"
                            inline
                            id="selected"
                            checked={ccwnotification.selected || false}
                            disabled={
                              !(
                                this.props.currentUser.isSystemAdmin ||
                                ccwnotification.inspectorId === this.props.currentUser.id
                              )
                            }
                            updateState={(e) => {
                              this.togleSelected(e, ccwnotification);
                            }}
                          >
                            &nbsp;
                          </CheckboxControl>
                        </td>
                      </tr>
                    );
                  })}
                </SortTable>
              </React.Fragment>
            );
          })()}
        </div>
      </div>
    );
  }
}

function mapStateToProps(state) {
  return {
    currentUser: state.user,
    districts: state.lookups.districts,
    inspectors: state.lookups.inspectors,
    ccwnotifications: state.models.ccwnotifications,
    favourites: state.models.favourites,
    search: state.search.ccwnotifications,
    ui: state.ui.ccwnotifications,
    owners: state.lookups.owners,
  };
}

export default connect(mapStateToProps)(CCWNotifications);
