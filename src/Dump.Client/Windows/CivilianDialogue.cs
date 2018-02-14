﻿using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Dispatch.Common.DataHolders.Storage;

namespace DispatchSystem.Dump.Client.Windows
{
    public partial class CivilianDialogue : Form
    {
        public CivilianDialogue(IEnumerable<Civilian> civs)
        {
            InitializeComponent();

            if (civs == null)
            {
                MessageBox.Show("ERROR: Civilian list is null! This is an immediate issue please contact BlockBa5her",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var civ in civs)
            {
                ListViewItem item = new ListViewItem(civ?.Id.ToString() ?? "NULL");
                item.SubItems.Add(civ?.License ?? "NULL");
                item.SubItems.Add(civ?.First ?? "NULL");
                item.SubItems.Add(civ?.Last ?? "NULL");
                item.SubItems.Add(civ?.WarrantStatus.ToString() ?? "NULL");
                item.SubItems.Add(civ?.Creation.ToString(CultureInfo.InvariantCulture) ?? "NULL");

                string notes = string.Empty;
                if (civ != null)
                    for (int i = 0; i < civ.Notes.Count; i++)
                        notes += (i == 0 ? "" : " ;;; ") + civ.Notes[i];
                else
                    notes = "NULL";
                item.SubItems.Add(notes);

                string tickets = string.Empty;
                if (civ != null)
                    for (int i = 0; i < civ.Tickets.Count; i++)
                        tickets += (i == 0 ? "" : " ;;; ") + $"${civ?.Tickets[i].Amount} | {civ?.Tickets[i].Reason}";
                else
                    tickets = "NULL";
                item.SubItems.Add(tickets);

                a.Items.Add(item);
            }
        }
    }
}
