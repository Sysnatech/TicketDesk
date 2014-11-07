﻿// TicketDesk - Attribution notice
// Contributor(s):
//
//      Stephen Redd (stephen@reddnet.net, http://www.reddnet.net)
//
// This file is distributed under the terms of the Microsoft Public 
// License (Ms-PL). See http://ticketdesk.codeplex.com/license
// for the complete terms of use. 
//
// For any distribution that contains code from this file, this notice of 
// attribution must remain intact, and a copy of the license must be 
// provided to the recipient.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using TicketDesk.Domain.Models;

namespace TicketDesk.Domain.Model
{
    /// <summary>
    /// Class UserTicketListSettingsCollection.
    /// </summary>
    /// <remarks>
    /// This class is a mapped as a complex type, which allows storing the list settings 
    /// to the DB as JSON, while treating the data in EF as regular entities
    /// </remarks>
    public class UserTicketListSettingsCollection: Collection<UserTicketListSetting>
    {
        /// <summary>
        /// Adds the specified settings.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public void Add(ICollection<UserTicketListSetting> settings)
        {
            foreach (var listSetting in settings)
            {
                this.Add(listSetting);
            }
        }

        /// <summary>
        /// Gets or sets the json serialized representation of the entire collection.
        /// </summary>
        /// <remarks>
        /// This is the only value in the entire sub-model for list 
        /// settings that will be stored by EF to the physical database.
        /// </remarks>
        /// <value>The serialized json settings collection.</value>
        public string Serialized
        {
            get { return Newtonsoft.Json.JsonConvert.SerializeObject(this); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var jData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserTicketListSetting>>(value);
                this.Items.Clear();
                this.Add(jData);
                
            }
        }
    }
}