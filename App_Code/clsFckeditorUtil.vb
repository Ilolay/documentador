Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

Public Module Util
    Public Function Parse(Of T)(ByVal value As Object) As T
        Try
            Return DirectCast(System.ComponentModel.TypeDescriptor.GetConverter(GetType(T)).ConvertFrom(value.ToString()), T)
        Catch generatedExceptionName As Exception
            Return Nothing
        End Try
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToImage(ByVal data As Byte()) As Image
        Dim stream As New MemoryStream(data.Length)
        stream.Write(data, 0, data.Length)
        stream.Seek(0L, SeekOrigin.Begin)
        Return Image.FromStream(stream)
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToByteArray(ByVal img As Image) As Byte()
        Dim resized As New MemoryStream()
        img.Save(resized, ImageFormat.Jpeg)
        Return resized.ToArray()
    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function Resize(ByVal image As Image, ByVal width As System.Nullable(Of Integer), ByVal height As System.Nullable(Of Integer), ByVal background As System.Nullable(Of Color)) As Image
        If width Is Nothing AndAlso height Is Nothing OrElse width = 0 AndAlso height = 0 Then
            Return image
        End If

        Dim w As Integer = If((width Is Nothing OrElse width = 0), image.Width, width.Value)
        Dim h As Integer = If((height Is Nothing OrElse height = 0), image.Height, height.Value)
        Dim desiredRatio As Single = CSng(w) / h
        Dim scale As Single, posx As Single, posy As Single
        Dim ratio As Single = CSng(image.Width) / image.Height

        If image.Width < w AndAlso image.Height < h Then
            scale = 1.0F
            posy = (h - image.Height) / 2.0F
            posx = (w - image.Width) / 2.0F
        ElseIf ratio > desiredRatio Then
            scale = CSng(w) / image.Width
            posy = (h - (image.Height * scale)) / 2.0F
            posx = 0.0F
        Else
            scale = CSng(h) / image.Height
            posx = (w - (image.Width * scale)) / 2.0F
            posy = 0.0F
        End If

        If Not background.HasValue Then
            w = CInt((image.Width * scale))
            h = CInt((image.Height * scale))
            posx = 0.0F
            posy = 0.0F
        End If

        Dim resizedImage As Image = New Bitmap(w, h)
        Dim g As Graphics = Graphics.FromImage(resizedImage)
        g.SmoothingMode = SmoothingMode.HighQuality
        g.InterpolationMode = InterpolationMode.HighQualityBicubic
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        If background.HasValue Then
            g.FillRectangle(New SolidBrush(background.Value), 0, 0, w, h)
        End If

        g.DrawImage(image, posx, posy, image.Width * scale, image.Height * scale)

        For Each item As PropertyItem In image.PropertyItems
            resizedImage.SetPropertyItem(item)
        Next

        Return resizedImage
    End Function
End Module

Public Class Config
    Public Sub New()
        '
        ' TODO: Add constructor logic here
        '
    End Sub
End Class


