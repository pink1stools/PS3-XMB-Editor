using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Syncfusion.Licensing;

namespace PS3_XMB_Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzMwOTlAMzEzNjJlMzQyZTMwZGswcUtuQXRKVGRnS0lNbVZIQmpPdEtlZHhTeHBBSGRvczVUcCtrbVJhdz0=;NzMxMDBAMzEzNjJlMzQyZTMwWXZWUmxuSjRvejV6N0tSbWp5R2JTeWFkclJOMDIvMjFwZmpwS2xiYlhzTT0=;NzMxMDFAMzEzNjJlMzQyZTMwZk9HUzVSZG9UK0xNZG1GU3R6dHJvTFBzRlRZY0wxYzlCRnY3NUdITzhDcz0=;NzMxMDJAMzEzNjJlMzQyZTMwalNrM0twLzBHbnRmc3p2M29VN2U2S1N3QVVSUUdlb0ExUzVta0lmbnNWRT0=;NzMxMDNAMzEzNjJlMzQyZTMwTlFmUXlMbGJOSnNLRDZ4YXpJRDEzS1JHZ3ROb1I5ODVkdjJmQVhUMVlVMD0=;NzMxMDRAMzEzNjJlMzQyZTMwbEJjUFdrSHE2UEFseEJ2UmZWMnNsaExZTzJaazI3R1FLY3YxUkV5NzJhQT0=;NzMxMDVAMzEzNjJlMzQyZTMwZVdmdmFNVklUN1o3OC9CUkgwdHg0aHozSU5iZG53R1ZKL0dQSzJMdFMrOD0=;NzMxMDZAMzEzNjJlMzQyZTMwY3h0ZFQvd1RsKzFlQzA3d0gyd1hzS0RXUXlFZk9xbmxPS20vTjJ5aStYOD0=;NzMxMDdAMzEzNjJlMzQyZTMwV1ZoS1JRT2wyUXRYTVd2MTRNbTBzamN5MWdYMzRVdmx5V2s4MkhqLzR5WT0=");
        }
    }
}
