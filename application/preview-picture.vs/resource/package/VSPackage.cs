﻿
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Imaging;
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
            public const string COPYRIGHT = "Copyright (c) 2020-2022 by Viacheslav Lozinskyi. All rights reserved.";
            public const string DESCRIPTION = "Quick preview the most popular picture files";
            public const string GUID = "3A2DFCCD-20AE-48B2-871E-91F71042D6DD";
            public const string NAME = "Preview-PICTURE";
            public const string VERSION = "1.0.4";
        }

        internal class InfoBarService : IVsInfoBarUIEvents
        {
            private static uint s_Cookie = 0;

            public static void Validate()
            {
                try
                {
                    ThreadHelper.ThrowIfNotOnUIThread();
                    if (string.IsNullOrEmpty(atom.Trace.GetFailState(CONSTANT.APPLICATION)) == false)
                    {
                        var a_Context1 = Package.GetGlobalService(typeof(SVsInfoBarUIFactory)) as IVsInfoBarUIFactory;
                        var a_Context2 = (IVsInfoBarHost)null;
                        if (a_Context1 != null)
                        {
                            var a_Context3 = Package.GetGlobalService(typeof(SVsShell)) as IVsShell;
                            var a_Context4 = (object)null;
                            if (a_Context3 != null)
                            {
                                a_Context3.GetProperty((int)__VSSPROPID7.VSSPROPID_MainWindowInfoBarHost, out a_Context4);
                                a_Context2 = (IVsInfoBarHost)a_Context4;
                            }
                        }
                        if (a_Context2 != null)
                        {
                            var a_Context3 = a_Context1.CreateInfoBar(new InfoBarModel(
                                textSpans: new[]
                                {
                                    new InfoBarTextSpan(CONSTANT.NAME, true),
                                    new InfoBarTextSpan(" extension doesn't work without "),
                                    new InfoBarTextSpan("MetaOutput", true),
                                    new InfoBarTextSpan("! Please install it.")
                                },
                                actionItems: new[]
                                {
                                    new InfoBarButton("Install MetaOutput")
                                },
                                image: KnownMonikers.StatusError));
                            {
                                a_Context3.Advise(new InfoBarService(), out s_Cookie);
                                a_Context2.AddInfoBar(a_Context3);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            public void OnClosed(IVsInfoBarUIElement infoBar)
            {
                try
                {
                    infoBar.Unadvise(s_Cookie);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }

            public void OnActionItemClicked(IVsInfoBarUIElement infoBar, IVsInfoBarActionItem action)
            {
                try
                {
                    Process.Start(atom.Trace.GetFailState(CONSTANT.APPLICATION));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
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
            {
                InfoBarService.Validate();
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
