using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace resource.package
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(CONSTANT.GUID)]
    [ProvideAutoLoad(VSConstants.UICONTEXT.ShellInitialized_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class PreviewPICTURE : AsyncPackage
    {
        internal static class CONSTANT
        {
            public const string APPLICATION = "Visual Studio";
            public const string COMPANY = "Viacheslav Lozinskyi";
            public const string COPYRIGHT = "Copyright (c) 2020-2023 by Viacheslav Lozinskyi. All rights reserved.";
            public const string DESCRIPTION = "Quick preview the most popular picture files";
            public const string GUID = "3A2DFCCD-20AE-48B2-871E-91F71042D6DD";
            public const string HOST = "MetaOutput";
            public const string NAME = "Preview-PICTURE";
            public const string VERSION = "1.1.0";
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            extension.AnyPreview.Connect(CONSTANT.APPLICATION, CONSTANT.NAME);
            extension.AnyPreview.Register(".BMP", new resource.preview.Native());
            extension.AnyPreview.Register(".CUR", new resource.preview.Native());
            extension.AnyPreview.Register(".DNG", new resource.preview.Taglib());
            extension.AnyPreview.Register(".GIF", new resource.preview.Taglib());
            extension.AnyPreview.Register(".ICO", new resource.preview.Native());
            extension.AnyPreview.Register(".JFIF", new resource.preview.Taglib());
            extension.AnyPreview.Register(".JPE", new resource.preview.Taglib());
            extension.AnyPreview.Register(".JPG", new resource.preview.Taglib());
            extension.AnyPreview.Register(".JPEG", new resource.preview.Taglib());
            extension.AnyPreview.Register(".PNG", new resource.preview.Taglib());
            extension.AnyPreview.Register(".SVG", new resource.preview.SVG());
            extension.AnyPreview.Register(".TIF", new resource.preview.Taglib());
            extension.AnyPreview.Register(".TIFF", new resource.preview.Taglib());
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
        }

        protected override int QueryClose(out bool canClose)
        {
            extension.AnyPreview.Disconnect();
            canClose = true;
            return VSConstants.S_OK;
        }
    }
}
