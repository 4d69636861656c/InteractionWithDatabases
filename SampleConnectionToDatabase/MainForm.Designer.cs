
namespace SampleConnectionToDatabase
{
    partial class MainForm
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
            this.createUserButton = new System.Windows.Forms.Button();
            this.usersDataGridView = new System.Windows.Forms.DataGridView();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.usersTabPage = new System.Windows.Forms.TabPage();
            this.deleteUserButton = new System.Windows.Forms.Button();
            this.updateUserButton = new System.Windows.Forms.Button();
            this.phoneLabel = new System.Windows.Forms.Label();
            this.phoneTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.userPasswordLabel = new System.Windows.Forms.Label();
            this.userPasswordTextBox = new System.Windows.Forms.TextBox();
            this.userNameLabel = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.productsTabPage = new System.Windows.Forms.TabPage();
            this.productCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.productCategoryLabel = new System.Windows.Forms.Label();
            this.deleteProductButton = new System.Windows.Forms.Button();
            this.updateProductButton = new System.Windows.Forms.Button();
            this.productDescriptionLabel = new System.Windows.Forms.Label();
            this.productDescriptionTextBox = new System.Windows.Forms.TextBox();
            this.productDiscountLabel = new System.Windows.Forms.Label();
            this.productDiscountTextBox = new System.Windows.Forms.TextBox();
            this.productPriceLabel = new System.Windows.Forms.Label();
            this.productPriceTextBox = new System.Windows.Forms.TextBox();
            this.productNameLabel = new System.Windows.Forms.Label();
            this.productNameTextBox = new System.Windows.Forms.TextBox();
            this.productsDataGridView = new System.Windows.Forms.DataGridView();
            this.createProductButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).BeginInit();
            this.mainTabControl.SuspendLayout();
            this.usersTabPage.SuspendLayout();
            this.productsTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // createUserButton
            // 
            this.createUserButton.BackColor = System.Drawing.Color.Green;
            this.createUserButton.Location = new System.Drawing.Point(660, 346);
            this.createUserButton.Name = "createUserButton";
            this.createUserButton.Size = new System.Drawing.Size(102, 46);
            this.createUserButton.TabIndex = 0;
            this.createUserButton.Text = "Create User";
            this.createUserButton.UseVisualStyleBackColor = false;
            this.createUserButton.Click += new System.EventHandler(this.CreateUserClick);
            // 
            // usersDataGridView
            // 
            this.usersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersDataGridView.Location = new System.Drawing.Point(6, 6);
            this.usersDataGridView.Name = "usersDataGridView";
            this.usersDataGridView.Size = new System.Drawing.Size(756, 276);
            this.usersDataGridView.TabIndex = 1;
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.usersTabPage);
            this.mainTabControl.Controls.Add(this.productsTabPage);
            this.mainTabControl.Location = new System.Drawing.Point(12, 12);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(776, 424);
            this.mainTabControl.TabIndex = 2;
            // 
            // usersTabPage
            // 
            this.usersTabPage.Controls.Add(this.deleteUserButton);
            this.usersTabPage.Controls.Add(this.updateUserButton);
            this.usersTabPage.Controls.Add(this.phoneLabel);
            this.usersTabPage.Controls.Add(this.phoneTextBox);
            this.usersTabPage.Controls.Add(this.emailLabel);
            this.usersTabPage.Controls.Add(this.emailTextBox);
            this.usersTabPage.Controls.Add(this.userPasswordLabel);
            this.usersTabPage.Controls.Add(this.userPasswordTextBox);
            this.usersTabPage.Controls.Add(this.userNameLabel);
            this.usersTabPage.Controls.Add(this.userNameTextBox);
            this.usersTabPage.Controls.Add(this.usersDataGridView);
            this.usersTabPage.Controls.Add(this.createUserButton);
            this.usersTabPage.Location = new System.Drawing.Point(4, 22);
            this.usersTabPage.Name = "usersTabPage";
            this.usersTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.usersTabPage.Size = new System.Drawing.Size(768, 398);
            this.usersTabPage.TabIndex = 0;
            this.usersTabPage.Text = "Users";
            this.usersTabPage.UseVisualStyleBackColor = true;
            // 
            // deleteUserButton
            // 
            this.deleteUserButton.BackColor = System.Drawing.Color.Red;
            this.deleteUserButton.Location = new System.Drawing.Point(660, 294);
            this.deleteUserButton.Name = "deleteUserButton";
            this.deleteUserButton.Size = new System.Drawing.Size(102, 46);
            this.deleteUserButton.TabIndex = 11;
            this.deleteUserButton.Text = "Delete User";
            this.deleteUserButton.UseVisualStyleBackColor = false;
            this.deleteUserButton.Click += new System.EventHandler(this.DeleteUserButton_Click);
            // 
            // updateUserButton
            // 
            this.updateUserButton.BackColor = System.Drawing.Color.SteelBlue;
            this.updateUserButton.Location = new System.Drawing.Point(552, 346);
            this.updateUserButton.Name = "updateUserButton";
            this.updateUserButton.Size = new System.Drawing.Size(102, 46);
            this.updateUserButton.TabIndex = 10;
            this.updateUserButton.Text = "Update User";
            this.updateUserButton.UseVisualStyleBackColor = false;
            this.updateUserButton.Click += new System.EventHandler(this.UpdateUserButton_Click);
            // 
            // phoneLabel
            // 
            this.phoneLabel.AutoSize = true;
            this.phoneLabel.Location = new System.Drawing.Point(286, 375);
            this.phoneLabel.Name = "phoneLabel";
            this.phoneLabel.Size = new System.Drawing.Size(38, 13);
            this.phoneLabel.TabIndex = 9;
            this.phoneLabel.Text = "Phone";
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.Location = new System.Drawing.Point(369, 372);
            this.phoneTextBox.MaxLength = 15;
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.Size = new System.Drawing.Size(141, 20);
            this.phoneTextBox.TabIndex = 8;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Location = new System.Drawing.Point(286, 349);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(32, 13);
            this.emailLabel.TabIndex = 7;
            this.emailLabel.Text = "Email";
            // 
            // emailTextBox
            // 
            this.emailTextBox.Location = new System.Drawing.Point(369, 346);
            this.emailTextBox.MaxLength = 100;
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(141, 20);
            this.emailTextBox.TabIndex = 6;
            // 
            // userPasswordLabel
            // 
            this.userPasswordLabel.AutoSize = true;
            this.userPasswordLabel.Location = new System.Drawing.Point(6, 375);
            this.userPasswordLabel.Name = "userPasswordLabel";
            this.userPasswordLabel.Size = new System.Drawing.Size(78, 13);
            this.userPasswordLabel.TabIndex = 5;
            this.userPasswordLabel.Text = "User Password";
            // 
            // userPasswordTextBox
            // 
            this.userPasswordTextBox.Location = new System.Drawing.Point(90, 372);
            this.userPasswordTextBox.MaxLength = 50;
            this.userPasswordTextBox.Name = "userPasswordTextBox";
            this.userPasswordTextBox.Size = new System.Drawing.Size(141, 20);
            this.userPasswordTextBox.TabIndex = 4;
            // 
            // userNameLabel
            // 
            this.userNameLabel.AutoSize = true;
            this.userNameLabel.Location = new System.Drawing.Point(6, 349);
            this.userNameLabel.Name = "userNameLabel";
            this.userNameLabel.Size = new System.Drawing.Size(60, 13);
            this.userNameLabel.TabIndex = 3;
            this.userNameLabel.Text = "User Name";
            // 
            // userNameTextBox
            // 
            this.userNameTextBox.Location = new System.Drawing.Point(90, 346);
            this.userNameTextBox.MaxLength = 35;
            this.userNameTextBox.Name = "userNameTextBox";
            this.userNameTextBox.Size = new System.Drawing.Size(141, 20);
            this.userNameTextBox.TabIndex = 2;
            // 
            // productsTabPage
            // 
            this.productsTabPage.Controls.Add(this.productCategoryComboBox);
            this.productsTabPage.Controls.Add(this.productCategoryLabel);
            this.productsTabPage.Controls.Add(this.deleteProductButton);
            this.productsTabPage.Controls.Add(this.updateProductButton);
            this.productsTabPage.Controls.Add(this.productDescriptionLabel);
            this.productsTabPage.Controls.Add(this.productDescriptionTextBox);
            this.productsTabPage.Controls.Add(this.productDiscountLabel);
            this.productsTabPage.Controls.Add(this.productDiscountTextBox);
            this.productsTabPage.Controls.Add(this.productPriceLabel);
            this.productsTabPage.Controls.Add(this.productPriceTextBox);
            this.productsTabPage.Controls.Add(this.productNameLabel);
            this.productsTabPage.Controls.Add(this.productNameTextBox);
            this.productsTabPage.Controls.Add(this.productsDataGridView);
            this.productsTabPage.Controls.Add(this.createProductButton);
            this.productsTabPage.Location = new System.Drawing.Point(4, 22);
            this.productsTabPage.Name = "productsTabPage";
            this.productsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.productsTabPage.Size = new System.Drawing.Size(768, 398);
            this.productsTabPage.TabIndex = 1;
            this.productsTabPage.Text = "Products";
            this.productsTabPage.UseVisualStyleBackColor = true;
            // 
            // productCategoryComboBox
            // 
            this.productCategoryComboBox.FormattingEnabled = true;
            this.productCategoryComboBox.Location = new System.Drawing.Point(90, 345);
            this.productCategoryComboBox.Name = "productCategoryComboBox";
            this.productCategoryComboBox.Size = new System.Drawing.Size(141, 21);
            this.productCategoryComboBox.TabIndex = 26;
            // 
            // productCategoryLabel
            // 
            this.productCategoryLabel.AutoSize = true;
            this.productCategoryLabel.Location = new System.Drawing.Point(6, 349);
            this.productCategoryLabel.Name = "productCategoryLabel";
            this.productCategoryLabel.Size = new System.Drawing.Size(49, 13);
            this.productCategoryLabel.TabIndex = 25;
            this.productCategoryLabel.Text = "Category";
            // 
            // deleteProductButton
            // 
            this.deleteProductButton.BackColor = System.Drawing.Color.Red;
            this.deleteProductButton.Location = new System.Drawing.Point(660, 294);
            this.deleteProductButton.Name = "deleteProductButton";
            this.deleteProductButton.Size = new System.Drawing.Size(102, 46);
            this.deleteProductButton.TabIndex = 23;
            this.deleteProductButton.Text = "Delete Product";
            this.deleteProductButton.UseVisualStyleBackColor = false;
            // 
            // updateProductButton
            // 
            this.updateProductButton.BackColor = System.Drawing.Color.SteelBlue;
            this.updateProductButton.Location = new System.Drawing.Point(552, 346);
            this.updateProductButton.Name = "updateProductButton";
            this.updateProductButton.Size = new System.Drawing.Size(102, 46);
            this.updateProductButton.TabIndex = 22;
            this.updateProductButton.Text = "Update Product";
            this.updateProductButton.UseVisualStyleBackColor = false;
            this.updateProductButton.Click += new System.EventHandler(this.UpdateProductButton_Click);
            // 
            // productDescriptionLabel
            // 
            this.productDescriptionLabel.AutoSize = true;
            this.productDescriptionLabel.Location = new System.Drawing.Point(286, 375);
            this.productDescriptionLabel.Name = "productDescriptionLabel";
            this.productDescriptionLabel.Size = new System.Drawing.Size(60, 13);
            this.productDescriptionLabel.TabIndex = 21;
            this.productDescriptionLabel.Text = "Description";
            // 
            // productDescriptionTextBox
            // 
            this.productDescriptionTextBox.Location = new System.Drawing.Point(369, 372);
            this.productDescriptionTextBox.MaxLength = 500;
            this.productDescriptionTextBox.Name = "productDescriptionTextBox";
            this.productDescriptionTextBox.Size = new System.Drawing.Size(141, 20);
            this.productDescriptionTextBox.TabIndex = 20;
            // 
            // productDiscountLabel
            // 
            this.productDiscountLabel.AutoSize = true;
            this.productDiscountLabel.Location = new System.Drawing.Point(286, 349);
            this.productDiscountLabel.Name = "productDiscountLabel";
            this.productDiscountLabel.Size = new System.Drawing.Size(49, 13);
            this.productDiscountLabel.TabIndex = 19;
            this.productDiscountLabel.Text = "Discount";
            // 
            // productDiscountTextBox
            // 
            this.productDiscountTextBox.Location = new System.Drawing.Point(369, 346);
            this.productDiscountTextBox.MaxLength = 2;
            this.productDiscountTextBox.Name = "productDiscountTextBox";
            this.productDiscountTextBox.Size = new System.Drawing.Size(141, 20);
            this.productDiscountTextBox.TabIndex = 18;
            // 
            // productPriceLabel
            // 
            this.productPriceLabel.AutoSize = true;
            this.productPriceLabel.Location = new System.Drawing.Point(6, 375);
            this.productPriceLabel.Name = "productPriceLabel";
            this.productPriceLabel.Size = new System.Drawing.Size(71, 13);
            this.productPriceLabel.TabIndex = 17;
            this.productPriceLabel.Text = "Product Price";
            // 
            // productPriceTextBox
            // 
            this.productPriceTextBox.Location = new System.Drawing.Point(90, 372);
            this.productPriceTextBox.MaxLength = 8;
            this.productPriceTextBox.Name = "productPriceTextBox";
            this.productPriceTextBox.Size = new System.Drawing.Size(141, 20);
            this.productPriceTextBox.TabIndex = 16;
            // 
            // productNameLabel
            // 
            this.productNameLabel.AutoSize = true;
            this.productNameLabel.Location = new System.Drawing.Point(6, 323);
            this.productNameLabel.Name = "productNameLabel";
            this.productNameLabel.Size = new System.Drawing.Size(75, 13);
            this.productNameLabel.TabIndex = 15;
            this.productNameLabel.Text = "Product Name";
            // 
            // productNameTextBox
            // 
            this.productNameTextBox.Location = new System.Drawing.Point(90, 320);
            this.productNameTextBox.MaxLength = 25;
            this.productNameTextBox.Name = "productNameTextBox";
            this.productNameTextBox.Size = new System.Drawing.Size(141, 20);
            this.productNameTextBox.TabIndex = 14;
            // 
            // productsDataGridView
            // 
            this.productsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productsDataGridView.Location = new System.Drawing.Point(6, 6);
            this.productsDataGridView.Name = "productsDataGridView";
            this.productsDataGridView.Size = new System.Drawing.Size(756, 276);
            this.productsDataGridView.TabIndex = 13;
            // 
            // createProductButton
            // 
            this.createProductButton.BackColor = System.Drawing.Color.Green;
            this.createProductButton.Location = new System.Drawing.Point(660, 346);
            this.createProductButton.Name = "createProductButton";
            this.createProductButton.Size = new System.Drawing.Size(102, 46);
            this.createProductButton.TabIndex = 12;
            this.createProductButton.Text = "Create Product";
            this.createProductButton.UseVisualStyleBackColor = false;
            this.createProductButton.Click += new System.EventHandler(this.CreateProductButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 449);
            this.Controls.Add(this.mainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "MainForm";
            this.Text = "Main Window";
            ((System.ComponentModel.ISupportInitialize)(this.usersDataGridView)).EndInit();
            this.mainTabControl.ResumeLayout(false);
            this.usersTabPage.ResumeLayout(false);
            this.usersTabPage.PerformLayout();
            this.productsTabPage.ResumeLayout(false);
            this.productsTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productsDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button createUserButton;
        private System.Windows.Forms.DataGridView usersDataGridView;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage usersTabPage;
        private System.Windows.Forms.TabPage productsTabPage;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label userPasswordLabel;
        private System.Windows.Forms.TextBox userPasswordTextBox;
        private System.Windows.Forms.Label userNameLabel;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Label phoneLabel;
        private System.Windows.Forms.TextBox phoneTextBox;
        private System.Windows.Forms.Button updateUserButton;
        private System.Windows.Forms.Button deleteUserButton;
        private System.Windows.Forms.Button deleteProductButton;
        private System.Windows.Forms.Button updateProductButton;
        private System.Windows.Forms.Label productDescriptionLabel;
        private System.Windows.Forms.TextBox productDescriptionTextBox;
        private System.Windows.Forms.Label productDiscountLabel;
        private System.Windows.Forms.TextBox productDiscountTextBox;
        private System.Windows.Forms.Label productPriceLabel;
        private System.Windows.Forms.TextBox productPriceTextBox;
        private System.Windows.Forms.Label productNameLabel;
        private System.Windows.Forms.TextBox productNameTextBox;
        private System.Windows.Forms.DataGridView productsDataGridView;
        private System.Windows.Forms.Button createProductButton;
        private System.Windows.Forms.Label productCategoryLabel;
        private System.Windows.Forms.ComboBox productCategoryComboBox;
    }
}

