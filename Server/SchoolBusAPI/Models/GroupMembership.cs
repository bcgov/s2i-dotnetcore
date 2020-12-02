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
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolBusAPI.Models
{
    /// <summary>
    /// The users associated with a given group that has been defined in the application.
    /// </summary>
    [MetaDataExtension (Description = "The users associated with a given group that has been defined in the application.")]

    public partial class GroupMembership : AuditableEntity, IEquatable<GroupMembership>
    {
        /// <summary>
        /// Default constructor, required by entity framework
        /// </summary>
        public GroupMembership()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupMembership" /> class.
        /// </summary>
        /// <param name="Id">A system-generated unique identifier for a GroupMembership (required).</param>
        /// <param name="Active">A flag indicating the User is active in the group. Set false to remove the user from the designated group. (required).</param>
        /// <param name="Group">A foreign key reference to the system-generated unique identifier for a Group.</param>
        /// <param name="User">A foreign key reference to the system-generated unique identifier for a User.</param>
        public GroupMembership(int Id, bool Active, Group Group = null, User User = null)
        {   
            this.Id = Id;
            this.Active = Active;

            this.Group = Group;
            this.User = User;
        }

        /// <summary>
        /// A system-generated unique identifier for a GroupMembership
        /// </summary>
        /// <value>A system-generated unique identifier for a GroupMembership</value>
        [MetaDataExtension (Description = "A system-generated unique identifier for a GroupMembership")]
        public int Id { get; set; }
        
        /// <summary>
        /// A flag indicating the User is active in the group. Set false to remove the user from the designated group.
        /// </summary>
        /// <value>A flag indicating the User is active in the group. Set false to remove the user from the designated group.</value>
        [MetaDataExtension (Description = "A flag indicating the User is active in the group. Set false to remove the user from the designated group.")]
        public bool Active { get; set; }
        
        /// <summary>
        /// A foreign key reference to the system-generated unique identifier for a Group
        /// </summary>
        /// <value>A foreign key reference to the system-generated unique identifier for a Group</value>
        [MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Group")]
        public Group Group { get; set; }
        
        /// <summary>
        /// Foreign key for Group 
        /// </summary>   
        [ForeignKey("Group")]
		[JsonIgnore]
		[MetaDataExtension (Description = "A foreign key reference to the system-generated unique identifier for a Group")]
        public int? GroupId { get; set; }
        
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
            sb.Append("class GroupMembership {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
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
            return Equals((GroupMembership)obj);
        }

        /// <summary>
        /// Returns true if GroupMembership instances are equal
        /// </summary>
        /// <param name="other">Instance of GroupMembership to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(GroupMembership other)
        {

            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return                 
                (
                    this.Id == other.Id ||
                    this.Id.Equals(other.Id)
                ) &&                 
                (
                    this.Active == other.Active ||
                    this.Active.Equals(other.Active)
                ) &&                 
                (
                    this.Group == other.Group ||
                    this.Group != null &&
                    this.Group.Equals(other.Group)
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
                hash = hash * 59 + this.Active.GetHashCode();
                                   
                if (this.Group != null)
                {
                    hash = hash * 59 + this.Group.GetHashCode();
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
        public static bool operator ==(GroupMembership left, GroupMembership right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Not Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(GroupMembership left, GroupMembership right)
        {
            return !Equals(left, right);
        }

        #endregion Operators
    }
}
