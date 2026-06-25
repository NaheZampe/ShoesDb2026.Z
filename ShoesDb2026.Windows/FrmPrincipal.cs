using Microsoft.Extensions.DependencyInjection;

namespace ShoesDb2026.Windows
{
    public partial class FrmPrincipal : Form
    {
        private readonly IServiceProvider _serviceProvider;
        public FrmPrincipal(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void btnBrands_Click(object sender, EventArgs e)
        {
            using (var frm = _serviceProvider.GetRequiredService<FrmBrand>())
            {
                frm.Text="Brands";
                frm.ShowDialog();
            }
        }
    }
}
