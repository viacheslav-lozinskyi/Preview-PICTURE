
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics;
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
            {
                extension.AnyPreview.Connect();
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
            }
            {
                await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            }
            try
            {
                if (string.IsNullOrEmpty(atom.Trace.GetFailState(CONSTANT.APPLICATION)) == false)
                {
                    var a_Context = Package.GetGlobalService(typeof(SDTE)) as DTE2;
                    if (a_Context != null)
                    {
                        var a_Context1 = (OutputWindowPane)null;
                        for (var i = a_Context.ToolWindows.OutputWindow.OutputWindowPanes.Count; i >= 1; i--)
                        {
                            if (a_Context.ToolWindows.OutputWindow.OutputWindowPanes.Item(i).Name == CONSTANT.HOST)
                            {
                                a_Context1 = a_Context.ToolWindows.OutputWindow.OutputWindowPanes.Item(i);
                                break;
                            }
                        }
                        if (a_Context1 == null)
                        {
                            a_Context1 = a_Context.ToolWindows.OutputWindow.OutputWindowPanes.Add(CONSTANT.HOST);
                        }
                        if (a_Context1 != null)
                        {
                            a_Context1.OutputString("\r\n" + CONSTANT.NAME + " extension doesn't work without MetaOutput.\r\n    Please install it --> https://www.metaoutput.net/download\r\n");
                            a_Context1.Activate();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        protected override int QueryClose(out bool canClose)
        {
            {
                extension.AnyPreview.Disconnect();
                canClose = true;
            }
            return VSConstants.S_OK;
        }
    }
}
