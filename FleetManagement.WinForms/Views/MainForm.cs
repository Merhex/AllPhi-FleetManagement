using FleetManagement.WinForms.Queries;
using FleetManagement.WinForms.Responses;
using FleetManagement.WinForms.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FleetManagement.WinForms.Views
{
    public partial class MainForm : Form
    {
        private readonly IApiRequestService _apiRequestService;

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public MainForm(IApiRequestService apiRequestService)
        {
            InitializeComponent();
            Load += Form_Load;

            _apiRequestService = apiRequestService;
        }

        private async void Form_Load(object sender, EventArgs eventArgs)
        {
            var query = new DriversQuery
            {
                Page = Page,
                PageSize = PageSize
            };

            var response = await _apiRequestService.SendQuery<PaginatedResponse<DriverResponse>>(query);

            var source = new BindingSource { DataSource = response.Items };

            driversGrid.DataSource = source;
        }
    }
}
