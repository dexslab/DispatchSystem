﻿using MaterialSkin;

namespace DispatchSystem.Terminal.Windows
{
    partial class OfficerView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            this.nameView = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.nameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.btnResync = new MaterialSkin.Controls.MaterialFlatButton();
            this.radioOnDuty = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.radioOffDuty = new MaterialSkin.Controls.MaterialRadioButton();
            this.radioBusy = new MaterialSkin.Controls.MaterialRadioButton();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.materialListView1 = new MaterialSkin.Controls.MaterialListView();
            this.assCreate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assSummary = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.assignmentsMenuStrip = new MaterialSkin.Controls.MaterialContextMenuStrip();
            this.addAssignment = new System.Windows.Forms.ToolStripMenuItem();
            this.createAssignment = new System.Windows.Forms.ToolStripMenuItem();
            this.removeSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider3 = new MaterialSkin.Controls.MaterialDivider();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.clockedView = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.assignmentsMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameView
            // 
            this.nameView.Depth = 0;
            this.nameView.Hint = "Callsign";
            this.nameView.Location = new System.Drawing.Point(12, 111);
            this.nameView.MaxLength = 32767;
            this.nameView.MouseState = MaterialSkin.MouseState.HOVER;
            this.nameView.Name = "nameView";
            this.nameView.PasswordChar = '\0';
            this.nameView.ReadOnly = true;
            this.nameView.SelectedText = "";
            this.nameView.SelectionLength = 0;
            this.nameView.SelectionStart = 0;
            this.nameView.Size = new System.Drawing.Size(155, 23);
            this.nameView.TabIndex = 0;
            this.nameView.TabStop = false;
            this.nameView.UseSystemPasswordChar = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Depth = 0;
            this.nameLabel.Font = new System.Drawing.Font("Roboto", 11F);
            this.nameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.nameLabel.Location = new System.Drawing.Point(12, 78);
            this.nameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(67, 19);
            this.nameLabel.TabIndex = 1;
            this.nameLabel.Text = "Callsign:";
            // 
            // btnResync
            // 
            this.btnResync.AutoSize = true;
            this.btnResync.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResync.Depth = 0;
            this.btnResync.Icon = null;
            this.btnResync.Location = new System.Drawing.Point(471, 501);
            this.btnResync.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.btnResync.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnResync.Name = "btnResync";
            this.btnResync.Primary = false;
            this.btnResync.Size = new System.Drawing.Size(73, 36);
            this.btnResync.TabIndex = 2;
            this.btnResync.Text = "Resync";
            this.btnResync.UseVisualStyleBackColor = true;
            this.btnResync.Click += new System.EventHandler(this.OnResyncClick);
            // 
            // radioOnDuty
            // 
            this.radioOnDuty.AutoSize = true;
            this.radioOnDuty.Depth = 0;
            this.radioOnDuty.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioOnDuty.Location = new System.Drawing.Point(242, 104);
            this.radioOnDuty.Margin = new System.Windows.Forms.Padding(0);
            this.radioOnDuty.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioOnDuty.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioOnDuty.Name = "radioOnDuty";
            this.radioOnDuty.Ripple = true;
            this.radioOnDuty.Size = new System.Drawing.Size(77, 30);
            this.radioOnDuty.TabIndex = 3;
            this.radioOnDuty.TabStop = true;
            this.radioOnDuty.Text = "On Duty";
            this.radioOnDuty.UseVisualStyleBackColor = true;
            this.radioOnDuty.Click += new System.EventHandler(this.StatusClick);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(238, 78);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(56, 19);
            this.materialLabel1.TabIndex = 4;
            this.materialLabel1.Text = "Status:";
            // 
            // radioOffDuty
            // 
            this.radioOffDuty.AutoSize = true;
            this.radioOffDuty.Depth = 0;
            this.radioOffDuty.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioOffDuty.Location = new System.Drawing.Point(242, 134);
            this.radioOffDuty.Margin = new System.Windows.Forms.Padding(0);
            this.radioOffDuty.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioOffDuty.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioOffDuty.Name = "radioOffDuty";
            this.radioOffDuty.Ripple = true;
            this.radioOffDuty.Size = new System.Drawing.Size(79, 30);
            this.radioOffDuty.TabIndex = 5;
            this.radioOffDuty.TabStop = true;
            this.radioOffDuty.Text = "Off Duty";
            this.radioOffDuty.UseVisualStyleBackColor = true;
            this.radioOffDuty.Click += new System.EventHandler(this.StatusClick);
            // 
            // radioBusy
            // 
            this.radioBusy.AutoSize = true;
            this.radioBusy.Depth = 0;
            this.radioBusy.Font = new System.Drawing.Font("Roboto", 10F);
            this.radioBusy.Location = new System.Drawing.Point(240, 164);
            this.radioBusy.Margin = new System.Windows.Forms.Padding(0);
            this.radioBusy.MouseLocation = new System.Drawing.Point(-1, -1);
            this.radioBusy.MouseState = MaterialSkin.MouseState.HOVER;
            this.radioBusy.Name = "radioBusy";
            this.radioBusy.Ripple = true;
            this.radioBusy.Size = new System.Drawing.Size(59, 30);
            this.radioBusy.TabIndex = 6;
            this.radioBusy.TabStop = true;
            this.radioBusy.Text = "Busy";
            this.radioBusy.UseVisualStyleBackColor = true;
            this.radioBusy.Click += new System.EventHandler(this.StatusClick);
            // 
            // materialDivider1
            // 
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(200, 78);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(16, 115);
            this.materialDivider1.TabIndex = 7;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // materialListView1
            // 
            this.materialListView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.materialListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.assCreate,
            this.assSummary});
            this.materialListView1.ContextMenuStrip = this.assignmentsMenuStrip;
            this.materialListView1.Depth = 0;
            this.materialListView1.Font = new System.Drawing.Font("Roboto", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.materialListView1.FullRowSelect = true;
            this.materialListView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.materialListView1.Location = new System.Drawing.Point(12, 267);
            this.materialListView1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.materialListView1.MouseState = MaterialSkin.MouseState.OUT;
            this.materialListView1.Name = "materialListView1";
            this.materialListView1.OwnerDraw = true;
            this.materialListView1.Size = new System.Drawing.Size(533, 225);
            this.materialListView1.TabIndex = 8;
            this.materialListView1.UseCompatibleStateImageBehavior = false;
            this.materialListView1.View = System.Windows.Forms.View.Details;
            // 
            // assCreate
            // 
            this.assCreate.Text = "Creation";
            this.assCreate.Width = 102;
            // 
            // assSummary
            // 
            this.assSummary.Text = "Summary";
            this.assSummary.Width = 423;
            // 
            // assignmentsMenuStrip
            // 
            this.assignmentsMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.assignmentsMenuStrip.Depth = 0;
            this.assignmentsMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAssignment,
            this.createAssignment,
            this.removeSelectedToolStripMenuItem});
            this.assignmentsMenuStrip.MouseState = MaterialSkin.MouseState.HOVER;
            this.assignmentsMenuStrip.Name = "assignmentsMenuStrip";
            this.assignmentsMenuStrip.Size = new System.Drawing.Size(165, 92);
            // 
            // addAssignment
            // 
            this.addAssignment.Name = "addAssignment";
            this.addAssignment.Size = new System.Drawing.Size(164, 22);
            this.addAssignment.Text = "Add to Existing";
            this.addAssignment.Click += new System.EventHandler(this.OnAddToExistingClick);
            // 
            // createAssignment
            // 
            this.createAssignment.Name = "createAssignment";
            this.createAssignment.Size = new System.Drawing.Size(164, 22);
            this.createAssignment.Text = "Create New";
            this.createAssignment.Click += new System.EventHandler(this.OnCreateNewAssignment);
            // 
            // removeSelectedToolStripMenuItem
            // 
            this.removeSelectedToolStripMenuItem.Name = "removeSelectedToolStripMenuItem";
            this.removeSelectedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.removeSelectedToolStripMenuItem.Text = "Remove Selected";
            this.removeSelectedToolStripMenuItem.Click += new System.EventHandler(this.OnRemoveSelectedClick);
            // 
            // materialDivider2
            // 
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(12, 214);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(533, 16);
            this.materialDivider2.TabIndex = 9;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(8, 245);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(93, 19);
            this.materialLabel2.TabIndex = 10;
            this.materialLabel2.Text = "Assignment:";
            // 
            // materialDivider3
            // 
            this.materialDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider3.Depth = 0;
            this.materialDivider3.Location = new System.Drawing.Point(357, 78);
            this.materialDivider3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider3.Name = "materialDivider3";
            this.materialDivider3.Size = new System.Drawing.Size(16, 115);
            this.materialDivider3.TabIndex = 11;
            this.materialDivider3.Text = "materialDivider3";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel3.Location = new System.Drawing.Point(390, 78);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(68, 19);
            this.materialLabel3.TabIndex = 12;
            this.materialLabel3.Text = "Clocked:";
            // 
            // clockedView
            // 
            this.clockedView.Depth = 0;
            this.clockedView.Hint = "Clocked";
            this.clockedView.Location = new System.Drawing.Point(394, 111);
            this.clockedView.MaxLength = 32767;
            this.clockedView.MouseState = MaterialSkin.MouseState.HOVER;
            this.clockedView.Name = "clockedView";
            this.clockedView.PasswordChar = '\0';
            this.clockedView.ReadOnly = true;
            this.clockedView.SelectedText = "";
            this.clockedView.SelectionLength = 0;
            this.clockedView.SelectionStart = 0;
            this.clockedView.Size = new System.Drawing.Size(140, 23);
            this.clockedView.TabIndex = 13;
            this.clockedView.TabStop = false;
            this.clockedView.UseSystemPasswordChar = false;
            // 
            // OfficerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(557, 552);
            this.Controls.Add(this.clockedView);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialDivider3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.materialListView1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.radioBusy);
            this.Controls.Add(this.radioOffDuty);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.radioOnDuty);
            this.Controls.Add(this.btnResync);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameView);
            this.MaximizeBox = false;
            this.Name = "OfficerView";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Officer Panel";
            this.assignmentsMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialSingleLineTextField nameView;
        private MaterialSkin.Controls.MaterialLabel nameLabel;
        private MaterialSkin.Controls.MaterialFlatButton btnResync;
        private MaterialSkin.Controls.MaterialRadioButton radioOnDuty;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialRadioButton radioOffDuty;
        private MaterialSkin.Controls.MaterialRadioButton radioBusy;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialListView materialListView1;
        private System.Windows.Forms.ColumnHeader assCreate;
        private System.Windows.Forms.ColumnHeader assSummary;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialContextMenuStrip assignmentsMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addAssignment;
        private System.Windows.Forms.ToolStripMenuItem createAssignment;
        private MaterialSkin.Controls.MaterialDivider materialDivider3;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialSingleLineTextField clockedView;
        private System.Windows.Forms.ToolStripMenuItem removeSelectedToolStripMenuItem;
    }
}