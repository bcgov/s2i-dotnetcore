/*
 * REST API Documentation for the MOTI School Bus Application
 *
 * The School Bus application tracks that inspections are performed in a timely fashion. For each school bus the application tracks information about the bus (including data from ICBC, NSC, etc.), it's past and next inspection dates and results, contacts, and the inspector responsible for next inspecting the bus.
 *
 * OpenAPI spec version: v1
 * 
 * 
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolBusAPI.Models;

namespace SchoolBusAPI.Models
{
    /// <summary>
    /// Users associated with specific notifications, created at the time the notification record is created because of the user&amp;#39;s business relationship to the bus - e.g. the user is the currently assigned inspector, or the user is the manager for the District in which the bus is located. The rules for linking a user to a notification for a specific bus could vary over time based on the needs of the business. At minimum, it is expected the current inspector assigned to a bus will be notified of every notification for that bus.
    /// </summary>
        [MetaDataExtension (Description = "Users associated with specific notifications, created at the time the notification record is created because of the user&amp;#39;s business relationship to the bus - e.g. the user is the currently assigned inspector, or the user is the manager for the District in which the bus is located. The rules for linking a user to a notification for a specific bus could vary over time based on the needs of the business. At minimum, it is expected the current inspector assigned to a bus will be notified of every notification for that bus.")]

    public partial class Notification : AuditableEntity, IEquatable<Notification>
    {
        /// <summary>
        /// Default constructor, required by entity framework
        /// </summary>
        public Notification()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Notification" /> class.
        /// </summary>
        /// <param name="Id">A system-generated unique identifier for a Notification (required).</param>
        /// <param name="Event">A foreign key reference to the system-generated unique identifier for a Notification Event.</param>
        /// <param name="Event2">A foreign key reference to the system-generated unique identifier for a Notification Event.</param>
        /// <param name="HasBeenViewed">True if the user linked to the inspection has read the notification.</param>
        /// <param name="IsWatchNotification">TO BE REMOVED.</param>
        /// <param name="IsExpired">TO BE REMOVED.</param>
        /// <param name="IsAllDay">TO BE REMOVED.</param>
        /// <param name="PriorityCode">TO BE REMOVED.</param>
        /// <param name="User">A foreign key reference to the system-generated unique identifier for a User.</param>
        public Notification(int Id, NotificationEvent Event = null, NotificationEvent Event2 = null, bool? HasBeenViewed = null, bool? IsWatchNotification = null, bool? IsExpired = null, bool? IsAllDay = null, string PriorityCode = null, User User = null)
        {   
            this.Id = Id;
            this.Event = Event;
            this.Event2 = Event2;
            this.HasBeenViewed = HasBeenViewed;
            this.IsWatchNotification = IsWatchNotification;
            this.IsExpired = IsExpired;
            this.IsAllDay = IsAllDay;
            this.PriorityCode = PriorityCode;
            this.User = User;
        }

        /// <summary>
        /// A system-generated unique identifier for a Notification
        /// </summary>
        /// <value>A system-generated unique identifier for a Notification</value>
        [MetaDataExtension (Description = "A system-generated unique identifier for a Notification")]
        public int Id { get; set; }
        
        /// <summary>
        /// A foreign key reference to the system-generated unique identifier for a Notification Event
        /// </summary>
        /// <value>A foreign key reference to the system-generated unique identifier for a Notification Event</value>
        [MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Notification Event")]
        public NotificationEvent Event { get; set; }
        
        /// <summary>
        /// Foreign key for Event 
        /// </summary>   
        [ForeignKey("Event")]
		[JsonIgnore]
		[MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Notification Event")]
        public int? EventId { get; set; }
        
        /// <summary>
        /// A foreign key reference to the system-generated unique identifier for a Notification Event
        /// </summary>
        /// <value>A foreign key reference to the system-generated unique identifier for a Notification Event</value>
        [MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Notification Event")]
        public NotificationEvent Event2 { get; set; }
        
        /// <summary>
        /// Foreign key for Event2 
        /// </summary>   
        [ForeignKey("Event2")]
		[JsonIgnore]
		[MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Notification Event")]
        public int? Event2Id { get; set; }
        
        /// <summary>
        /// True if the user linked to the inspection has read the notification
        /// </summary>
        /// <value>True if the user linked to the inspection has read the notification</value>
        [MetaDataExtension (Description = "True if the user linked to the inspection has read the notification")]
        public bool? HasBeenViewed { get; set; }
        
        /// <summary>
        /// TO BE REMOVED
        /// </summary>
        /// <value>TO BE REMOVED</value>
        [MetaDataExtension (Description = "TO BE REMOVED")]
        public bool? IsWatchNotification { get; set; }
        
        /// <summary>
        /// TO BE REMOVED
        /// </summary>
        /// <value>TO BE REMOVED</value>
        [MetaDataExtension (Description = "TO BE REMOVED")]
        public bool? IsExpired { get; set; }
        
        /// <summary>
        /// TO BE REMOVED
        /// </summary>
        /// <value>TO BE REMOVED</value>
        [MetaDataExtension (Description = "TO BE REMOVED")]
        public bool? IsAllDay { get; set; }
        
        /// <summary>
        /// TO BE REMOVED
        /// </summary>
        /// <value>TO BE REMOVED</value>
        [MetaDataExtension (Description = "TO BE REMOVED")]
        [MaxLength(255)]
        
        public string PriorityCode { get; set; }
        
        /// <summary>
        /// A foreign key reference to the system-generated unique identifier for a User
        /// </summary>
        /// <value>A foreign key reference to the system-generated unique identifier for a User</value>
        [MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a User")]
        public User User { get; set; }
        
        /// <summary>
        /// Foreign key for User 
        /// </summary>   
        [ForeignKey("User")]
		[JsonIgnore]
		[MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a User")]
        public int? UserId { get; set; }
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Notification {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Event: ").Append(Event).Append("\n");
            sb.Append("  Event2: ").Append(Event2).Append("\n");
            sb.Append("  HasBeenViewed: ").Append(HasBeenViewed).Append("\n");
            sb.Append("  IsWatchNotification: ").Append(IsWatchNotification).Append("\n");
            sb.Append("  IsExpired: ").Append(IsExpired).Append("\n");
            sb.Append("  IsAllDay: ").Append(IsAllDay).Append("\n");
            sb.Append("  PriorityCode: ").Append(PriorityCode).Append("\n");
            sb.Append("  User: ").Append(User).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) { return false; }
            if (ReferenceEquals(this, obj)) { return true; }
            if (obj.GetType() != GetType()) { return false; }
            return Equals((Notification)obj);
        }

        /// <summary>
        /// Returns true if Notification instances are equal
        /// </summary>
        /// <param name="other">Instance of Notification to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Notification other)
        {

            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return                 
                (
                    this.Id == other.Id ||
                    this.Id.Equals(other.Id)
                ) &&                 
                (
                    this.Event == other.Event ||
                    this.Event != null &&
                    this.Event.Equals(other.Event)
                ) &&                 
                (
                    this.Event2 == other.Event2 ||
                    this.Event2 != null &&
                    this.Event2.Equals(other.Event2)
                ) &&                 
                (
                    this.HasBeenViewed == other.HasBeenViewed ||
                    this.HasBeenViewed != null &&
                    this.HasBeenViewed.Equals(other.HasBeenViewed)
                ) &&                 
                (
                    this.IsWatchNotification == other.IsWatchNotification ||
                    this.IsWatchNotification != null &&
                    this.IsWatchNotification.Equals(other.IsWatchNotification)
                ) &&                 
                (
                    this.IsExpired == other.IsExpired ||
                    this.IsExpired != null &&
                    this.IsExpired.Equals(other.IsExpired)
                ) &&                 
                (
                    this.IsAllDay == other.IsAllDay ||
                    this.IsAllDay != null &&
                    this.IsAllDay.Equals(other.IsAllDay)
                ) &&                 
                (
                    this.PriorityCode == other.PriorityCode ||
                    this.PriorityCode != null &&
                    this.PriorityCode.Equals(other.PriorityCode)
                ) &&                 
                (
                    this.User == other.User ||
                    this.User != null &&
                    this.User.Equals(other.User)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            // credit: http://stackoverflow.com/a/263416/677735
            unchecked // Overflow is fine, just wrap
            {
                int hash = 41;
                // Suitable nullity checks
                                   
                hash = hash * 59 + this.Id.GetHashCode();                   
                if (this.Event != null)
                {
                    hash = hash * 59 + this.Event.GetHashCode();
                }                   
                if (this.Event2 != null)
                {
                    hash = hash * 59 + this.Event2.GetHashCode();
                }                if (this.HasBeenViewed != null)
                {
                    hash = hash * 59 + this.HasBeenViewed.GetHashCode();
                }                
                                if (this.IsWatchNotification != null)
                {
                    hash = hash * 59 + this.IsWatchNotification.GetHashCode();
                }                
                                if (this.IsExpired != null)
                {
                    hash = hash * 59 + this.IsExpired.GetHashCode();
                }                
                                if (this.IsAllDay != null)
                {
                    hash = hash * 59 + this.IsAllDay.GetHashCode();
                }                
                                if (this.PriorityCode != null)
                {
                    hash = hash * 59 + this.PriorityCode.GetHashCode();
                }                
                                   
                if (this.User != null)
                {
                    hash = hash * 59 + this.User.GetHashCode();
                }
                return hash;
            }
        }

        #region Operators
        
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Notification left, Notification right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Not Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Notification left, Notification right)
        {
            return !Equals(left, right);
        }

        #endregion Operators
    }
}
