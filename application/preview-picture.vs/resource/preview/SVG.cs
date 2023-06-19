using Svg;
using System;
using System.Drawing.Imaging;
using System.IO;

namespace resource.preview
{
    internal class SVG : extension.AnyPreview
    {
        protected override void _Execute(atom.Trace context, int level, string url, string file)
        {
            var a_Context = SvgDocument.Open(file);
            var a_Name = atom.Trace.GetUrlTemp(file, ".png");
            {
                var a_Context1 = a_Context.Draw();
                if (a_Context1 != null)
                {
                    a_Context1.Save(a_Name, ImageFormat.Png);
                }
            }
            {
                context.
                    SetTrace(null, NAME.STATE.TRACE.BLINK).
                    SetProgress(CONSTANT.PROGRESS.INFINITE).
                    SetUrlPreview(a_Name).
                    SendPreview(NAME.EVENT.INFO, url);
            }
            {
                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.HEADER, level, "[[[Info]]]");
                {
                    context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 1, "[[[File Name]]]", url);
                    context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 1, "[[[File Size]]]", (new FileInfo(file)).Length.ToString());
                    context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 1, "[[[Raw Format]]]", "SVG");
                }
            }
            {
                var a_Count = GetProperty(NAME.PROPERTY.PREVIEW_MEDIA_SIZE, true);
                {
                    a_Count = Math.Min(a_Count, (int)a_Context.Bounds.Height / CONSTANT.OUTPUT.PREVIEW_ITEM_HEIGHT);
                    a_Count = Math.Max(a_Count, CONSTANT.OUTPUT.PREVIEW_MIN_SIZE);
                }
                {
                    context.
                        SetControl(NAME.CONTROL.PICTURE).
                        SetCount(a_Count).
                        SetUrlPreview(file).
                        Send(NAME.SOURCE.PREVIEW, NAME.EVENT.CONTROL, level);
                }
            }
            {
                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.FOOTER, level, "[[[Size]]]: " + ((int)a_Context.Bounds.Width).ToString() + " x " + ((int)a_Context.Bounds.Height).ToString());
                {
                    context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.FOLDER, level + 1, "[[[Codecs]]]");
                    {
                        context.
                            SetComment("[[[Picture]]]", "[[[Media Type]]]").
                            Send(NAME.SOURCE.PREVIEW, NAME.EVENT.FILE, level + 2, "SVG [[[File]]]");
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Header]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Baseline Shift]]]", a_Context.BaselineShift);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Display]]]", a_Context.Display);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[External]]] CSS", a_Context.ExternalCSSHref);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Id]]]", a_Context.ID);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Opacity]]]", a_Context.Opacity.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Overflow]]]", GetIdentifierString(a_Context.Overflow.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Pixel Per Inch]]]", a_Context.Ppi.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Shape Rendering]]]", GetIdentifierString(a_Context.ShapeRendering.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Space Handling]]]", GetIdentifierString(a_Context.SpaceHandling.ToString()));
                            }
                        }
                        if (a_Context.CustomAttributes?.Count > 0)
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Attributes]]]", a_Context.CustomAttributes?.ToString());
                            foreach (var a_Context1 in a_Context.CustomAttributes)
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.VARIABLE, level + 4, a_Context1.Key.ToString(), a_Context1.Value.ToString());
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Bounds]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Left]]]", a_Context.Bounds.Left.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Top]]]", a_Context.Bounds.Top.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Right]]]", a_Context.Bounds.Right.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Bottom]]]", a_Context.Bounds.Bottom.ToString());
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Color]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Current]]]", a_Context.Color?.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Interpolation]]]", GetIdentifierString(a_Context.ColorInterpolation.ToString()));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Fill]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Opacity]]]", a_Context.FillOpacity.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Rule]]]", GetIdentifierString(a_Context.FillRule.ToString()));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Font]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Name]]]", a_Context.Font);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Family]]]", a_Context.FontFamily);
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Size]]]", __GetString(a_Context.FontSize));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Stretch]]]", GetIdentifierString(a_Context.FontStretch.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Style]]]", GetIdentifierString(a_Context.FontStyle.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Variant]]]", GetIdentifierString(a_Context.FontVariant.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Weight]]]", GetIdentifierString(a_Context.FontWeight.ToString()));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Position]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "X", __GetString(a_Context.X));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "Y", __GetString(a_Context.Y));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Size]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Width]]]", __GetString(a_Context.Width));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Height]]]", __GetString(a_Context.Height));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Stroke]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Dash Offset]]]", __GetString(a_Context.StrokeDashOffset));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Line Cap]]]", GetIdentifierString(a_Context.StrokeLineCap.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Line Join]]]", GetIdentifierString(a_Context.StrokeLineJoin.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Miter Limit]]]", a_Context.StrokeMiterLimit.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Opacity]]]", a_Context.StrokeOpacity.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Width]]]", __GetString(a_Context.StrokeWidth));
                            }
                        }
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[Text]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Anchor]]]", GetIdentifierString(a_Context.TextAnchor.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Decoration]]]", GetIdentifierString(a_Context.TextDecoration.ToString()));
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Transformation]]]", GetIdentifierString(a_Context.TextTransformation.ToString()));
                            }
                        }
                        if (a_Context.ViewBox != null)
                        {
                            context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 3, "[[[View]]]");
                            {
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "X", a_Context.ViewBox.MinX.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "Y", a_Context.ViewBox.MinX.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Width]]]", a_Context.ViewBox.Width.ToString());
                                context.Send(NAME.SOURCE.PREVIEW, NAME.EVENT.PARAMETER, level + 4, "[[[Height]]]", a_Context.ViewBox.Height.ToString());
                            }
                        }
                    }
                }
            }
            {
                context.
                    SetTrace(null, NAME.STATE.TRACE.NONE).
                    SetProgress(100).
                    SetUrlPreview(a_Name).
                    SendPreview(NAME.EVENT.INFO, url);
            }
        }

        private static string __GetString(SvgUnit value)
        {
            if (value != null)
            {
                if (value.IsEmpty)
                {
                    return "[[[EMPTY]]]";
                }
                if (value.IsNone)
                {
                    return "[[[NONE]]]";
                }
                else
                {
                    return value.Value.ToString() + " [[[" + value.Type.ToString() + "]]]";
                }
            }
            return "";
        }

        private static string GetIdentifierString(string value)
        {
            return value;
        }
    };
}
