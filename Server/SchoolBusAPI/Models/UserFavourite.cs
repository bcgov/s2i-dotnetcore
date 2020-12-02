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
    /// User specific settings for a specific location in the UI. The location and saved settings are internally defined by the UI.
    /// </summary>
        [MetaDataExtension (Description = "User specific settings for a specific location in the UI. The location and saved settings are internally defined by the UI.")]

    public partial class UserFavourite : AuditableEntity, IEquatable<UserFavourite>
    {
        /// <summary>
        /// Default constructor, required by entity framework
        /// </summary>
        public UserFavourite()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserFavourite" /> class.
        /// </summary>
        /// <param name="Id">A system-generated unique identifier for a UserFavourite (required).</param>
        /// <param name="Type">The type of Favourite.</param>
        /// <param name="Name">The user-defined name for the recorded settings. Allows the user to save different groups of settings and access each one easily when needed..</param>
        /// <param name="Value">The settings saved by the user. In general,  a UI defined chunk of json that stores the settings in place when the user created the favourite..</param>
        /// <param name="IsDefault">True if this Favourite is the default for this Context Type. On first access to a context in a session the default favourite for the context it is invoked. If there is no default favourite,  a system-wide default is invoked. On return to the context within a session,  the last parameters used are reapplied..</param>
        /// <param name="User">The User who has this Favourite.</param>
        public UserFavourite(int Id, string Type = null, string Name = null, string Value = null, bool? IsDefault = null, User User = null)
        {   
            this.Id = Id;
            this.Type = Type;
            this.Name = Name;
            this.Value = Value;
            this.IsDefault = IsDefault;
            this.User = User;
        }

        /// <summary>
        /// A system-generated unique identifier for a UserFavourite
        /// </summary>
        /// <value>A system-generated unique identifier for a UserFavourite</value>
        [MetaDataExtension (Description = "A system-generated unique identifier for a UserFavourite")]
        public int Id { get; set; }
        
        /// <summary>
        /// The type of Favourite
        /// </summary>
        /// <value>The type of Favourite</value>
        [MetaDataExtension (Description = "The type of Favourite")]
        [MaxLength(255)]
        
        public string Type { get; set; }
        
        /// <summary>
        /// The user-defined name for the recorded settings. Allows the user to save different groups of settings and access each one easily when needed.
        /// </summary>
        /// <value>The user-defined name for the recorded settings. Allows the user to save different groups of settings and access each one easily when needed.</value>
        [MetaDataExtension (Description = "The user-defined name for the recorded settings. Allows the user to save different groups of settings and access each one easily when needed.")]
        [MaxLength(255)]
        
        public string Name { get; set; }
        
        /// <summary>
        /// The settings saved by the user. In general,  a UI defined chunk of json that stores the settings in place when the user created the favourite.
        /// </summary>
        /// <value>The settings saved by the user. In general,  a UI defined chunk of json that stores the settings in place when the user created the favourite.</value>
        [MetaDataExtension (Description = "The settings saved by the user. In general,  a UI defined chunk of json that stores the settings in place when the user created the favourite.")]
        [MaxLength(2048)]
        
        public string Value { get; set; }
        
        /// <summary>
        /// True if this Favourite is the default for this Context Type. On first access to a context in a session the default favourite for the context it is invoked. If there is no default favourite,  a system-wide default is invoked. On return to the context within a session,  the last parameters used are reapplied.
        /// </summary>
        /// <value>True if this Favourite is the default for this Context Type. On first access to a context in a session the default favourite for the context it is invoked. If there is no default favourite,  a system-wide default is invoked. On return to the context within a session,  the last parameters used are reapplied.</value>
        [MetaDataExtension (Description = "True if this Favourite is the default for this Context Type. On first access to a context in a session the default favourite for the context it is invoked. If there is no default favourite,  a system-wide default is invoked. On return to the context within a session,  the last parameters used are reapplied.")]
        public bool? IsDefault { get; set; }
        
        /// <summary>
        /// The User who has this Favourite
        /// </summary>
        /// <value>The User who has this Favourite</value>
        [MetaDataExtension (Description = "The User who has this Favourite")]
        public User User { get; set; }
        
        /// <summary>
        /// Foreign key for User 
        /// </summary>   
        [ForeignKey("User")]
		[JsonIgnore]
		[MetaDataExtension (Description = "The User who has this Favourite")]
        public int? UserId { get; set; }
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserFavourite {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  IsDefault: ").Append(IsDefault).Append("\n");
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
            return Equals((UserFavourite)obj);
        }

        /// <summary>
        /// Returns true if UserFavourite instances are equal
        /// </summary>
        /// <param name="other">Instance of UserFavourite to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UserFavourite other)
        {

            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return                 
                (
                    this.Id == other.Id ||
                    this.Id.Equals(other.Id)
                ) &&                 
                (
                    this.Type == other.Type ||
                    this.Type != null &&
                    this.Type.Equals(other.Type)
                ) &&                 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) &&                 
                (
                    this.Value == other.Value ||
                    this.Value != null &&
                    this.Value.Equals(other.Value)
                ) &&                 
                (
                    this.IsDefault == other.IsDefault ||
                    this.IsDefault != null &&
                    this.IsDefault.Equals(other.IsDefault)
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
                                   
                hash = hash * 59 + this.Id.GetHashCode();                if (this.Type != null)
                {
                    hash = hash * 59 + this.Type.GetHashCode();
                }                
                                if (this.Name != null)
                {
                    hash = hash * 59 + this.Name.GetHashCode();
                }                
                                if (this.Value != null)
                {
                    hash = hash * 59 + this.Value.GetHashCode();
                }                
                                if (this.IsDefault != null)
                {
                    hash = hash * 59 + this.IsDefault.GetHashCode();
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
        public static bool operator ==(UserFavourite left, UserFavourite right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Not Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(UserFavourite left, UserFavourite right)
        {
            return !Equals(left, right);
        }

        #endregion Operators
    }
}
