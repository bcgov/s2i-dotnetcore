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
    /// A table managed in the application by authorized users to create named Roles that can be assigned to Users as needed. Roles can be created as needed to support the users of the system and the roles they perform within the organization.
    /// </summary>
        [MetaDataExtension (Description = "A table managed in the application by authorized users to create named Roles that can be assigned to Users as needed. Roles can be created as needed to support the users of the system and the roles they perform within the organization.")]

    public partial class Role : AuditableEntity, IEquatable<Role>
    {
        /// <summary>
        /// Default constructor, required by entity framework
        /// </summary>
        public Role()
        {
            this.Id = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Role" /> class.
        /// </summary>
        /// <param name="Id">A system-generated unique identifier for a Role (required).</param>
        /// <param name="Name">The name of the Role, as established by the user creating the role. (required).</param>
        /// <param name="Description">A description of the role as set by the user creating&amp;#x2F;updating the role. (required).</param>
        /// <param name="RolePermissions">RolePermissions.</param>
        /// <param name="UserRoles">UserRoles.</param>
        public Role(int Id, string Name, string Description, List<RolePermission> RolePermissions = null, List<UserRole> UserRoles = null)
        {   
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;

            this.RolePermissions = RolePermissions;
            this.UserRoles = UserRoles;
        }

        /// <summary>
        /// A system-generated unique identifier for a Role
        /// </summary>
        /// <value>A system-generated unique identifier for a Role</value>
        [MetaDataExtension (Description = "A system-generated unique identifier for a Role")]
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the Role, as established by the user creating the role.
        /// </summary>
        /// <value>The name of the Role, as established by the user creating the role.</value>
        [MetaDataExtension (Description = "The name of the Role, as established by the user creating the role.")]
        [MaxLength(255)]
        
        public string Name { get; set; }
        
        /// <summary>
        /// A description of the role as set by the user creating&#x2F;updating the role.
        /// </summary>
        /// <value>A description of the role as set by the user creating&#x2F;updating the role.</value>
        [MetaDataExtension (Description = "A description of the role as set by the user creating&#x2F;updating the role.")]
        [MaxLength(255)]
        
        public string Description { get; set; }

        /// <summary>
        /// The date on which a role was removed.
        /// </summary>
        /// <value>The date on which a role was removed.</value>
        [MetaDataExtension(Description = "The date on which a role was removed")]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// Gets or Sets RolePermissions
        /// </summary>
        public List<RolePermission> RolePermissions { get; set; }
        
        /// <summary>
        /// Gets or Sets UserRoles
        /// </summary>
        public List<UserRole> UserRoles { get; set; }
        
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            return ToJson();
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
            return Equals((Role)obj);
        }

        /// <summary>
        /// Returns true if Role instances are equal
        /// </summary>
        /// <param name="other">Instance of Role to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Role other)
        {

            if (ReferenceEquals(null, other)) { return false; }
            if (ReferenceEquals(this, other)) { return true; }

            return                 
                (
                    this.Id == other.Id ||
                    this.Id.Equals(other.Id)
                ) &&                 
                (
                    this.Name == other.Name ||
                    this.Name != null &&
                    this.Name.Equals(other.Name)
                ) &&                 
                (
                    this.Description == other.Description ||
                    this.Description != null &&
                    this.Description.Equals(other.Description)
                ) && 
                (
                    this.RolePermissions == other.RolePermissions ||
                    this.RolePermissions != null &&
                    this.RolePermissions.SequenceEqual(other.RolePermissions)
                ) && 
                (
                    this.UserRoles == other.UserRoles ||
                    this.UserRoles != null &&
                    this.UserRoles.SequenceEqual(other.UserRoles)
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
                                   
                hash = hash * 59 + this.Id.GetHashCode();                if (this.Name != null)
                {
                    hash = hash * 59 + this.Name.GetHashCode();
                }                
                                if (this.Description != null)
                {
                    hash = hash * 59 + this.Description.GetHashCode();
                }                
                                   
                if (this.RolePermissions != null)
                {
                    hash = hash * 59 + this.RolePermissions.GetHashCode();
                }                   
                if (this.UserRoles != null)
                {
                    hash = hash * 59 + this.UserRoles.GetHashCode();
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
        public static bool operator ==(Role left, Role right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Not Equals
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Role left, Role right)
        {
            return !Equals(left, right);
        }

        #endregion Operators
    }
}
