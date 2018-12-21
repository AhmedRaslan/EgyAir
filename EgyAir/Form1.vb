Imports System.IO
Imports System.IO.IOException
Imports System.Windows.Forms.Control

Public Structure hagz
    Dim trip_no As Integer
    Dim trip_date As String
    Dim trip_time As String
    Dim arrive_time As String
    Dim city As String
    Dim city1 As String
    <VBFixedArray(59)> Dim korsi() As Integer
    <VBFixedArray(59)> Dim passenger() As String
    <VBFixedArray(59)> Dim phone() As String
    <VBFixedArray(59)> Dim address() As String
End Structure
Public Class Form1
    Inherits System.Windows.Forms.Form
    Dim buttons(59) As Button
    Dim z, i, s, X, J As Integer
    Dim position As Integer
    Dim lang As Boolean = True

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        FileClose(1)
        End
    End Sub

    Public prec As hagz
    Dim m, n, l As String

    Private Sub TextBox1_KeyPress(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If IsNumeric(TextBox1.Text) Then
            Try
                view()
            Catch ex As Exception
                If lang = True Then
                    MsgBox("Cannot find any trips associated to this number, you can create one !")
                Else
                    MsgBox("لا يمكن العثور على أي رحلات تحمل هذا الرقم, يمكنك انشاء واحدة !")
                End If

                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
                TextBox5.Clear()
                TextBox6.Clear()
            End Try
        Else
            clear()
            clear2()
            For i = 0 To 59

                buttons(i).BackgroundImage = My.Resources.Chair

            Next
        End If
    End Sub

    Private Sub Button63_Click(sender As Object, e As EventArgs) Handles Button63.Click
        If TextBox1.Text = "" Then
            If lang = True Then
                MsgBox("Some required fields are empty, please recheck !")
            Else
                MsgBox("بعض الحقول المطلوبة فارغة, رجاء اعد التحقق !")
            End If
            Exit Sub
        Else
            If prec.trip_no = Val(TextBox1.Text) Then
                If lang = True Then
                    MsgBox("There is already a trip associated to this number, change the number and try to add again !")
                Else
                    MsgBox("يوجد بالفعل رحلة تحمل هذا الرقم, قم بتغيير الرقم وحاول الإضافة مرة أخرى !")
                End If

                Exit Sub
            Else


                position = Loc(1)
                Seek(1, Val(TextBox1.Text))
                prec.trip_no = Val(TextBox1.Text)
                prec.trip_time = TextBox2.Text
                prec.trip_date = TextBox3.Text
                prec.arrive_time = TextBox4.Text
                prec.city = TextBox5.Text
                prec.city1 = TextBox6.Text
                For i = 0 To 59
                    prec.korsi(i) = 0
                    prec.passenger(i) = "0"
                    prec.phone(i) = "0"
                    prec.address(i) = "0"
                Next
                s = Val(TextBox1.Text)
                FilePut(1, prec, s)
                If lang = True Then
                    MsgBox("Trip added successfully !")
                Else
                    MsgBox("تمت إضافة الرحلة بنجاح !")
                End If
            End If
        End If
        clear()
    End Sub

    Private Sub Button64_Click(sender As Object, e As EventArgs) Handles Button64.Click
        If z = 1 Then
            If lang = True Then
                MsgBox("Sorry, this seat is already booked, cancel booking first !")
            Else
                MsgBox("نأسف, هذا المقعد بالفعل محجوز, قم بإلغاء حجزه أولا !")
            End If

        ElseIf z = 2 Then
            If lang = True Then
                MsgBox("Sorry, this seat is already booked and confirmed !")
            Else
                MsgBox("نأسف, هذا المقعد بالفعل محجوز وتم تأكيد حجزه !")
            End If

        ElseIf z = 0 Then
            If TextBox7.Text = "" Or TextBox8.Text = "" Or TextBox9.Text = "" Then
                If lang = True Then
                    MsgBox("Enter all the required details before booking, please !")
                Else
                    MsgBox("أدخل جميع التفاصيل المطلوبة قبل الحجز من فضلك !")
                End If

                Exit Sub
            End If
            If lang = True Then
                X = MsgBox("Do you want to book this seat ?!", MsgBoxStyle.OkCancel)
            Else
                X = MsgBox("هل تريد حجز هذا المقعد ؟!", MsgBoxStyle.OkCancel)
            End If

            If X = 1 Then
                buttons(J).BackgroundImage = My.Resources.ChairSelected
                z = 1
                FileGet(1, prec, Val(TextBox1.Text))
                prec.korsi(J) = z
                prec.passenger(J) = TextBox7.Text
                prec.phone(J) = TextBox8.Text
                prec.address(J) = TextBox9.Text
                s = Val(TextBox1.Text)
                FilePut(1, prec, s)
                clear2()
            ElseIf X = 2 Then
                Exit Sub
            End If
        End If

    End Sub

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        If z = 0 Then
            If lang = True Then
                MsgBox("You need to book this seat first !")
            Else
                MsgBox("أنت بحاجة لحجز هذا المقعد أولا !")
            End If

        ElseIf z = 1 Then
            If lang = True Then
                X = MsgBox("Confirm booking this seat ?!", MsgBoxStyle.OkCancel)
            Else
                X = MsgBox("تأكيد حجز هذا المقعد ؟!", MsgBoxStyle.OkCancel)
            End If

            If X = 1 Then
                buttons(J).BackgroundImage = My.Resources.ChairSelected
                z = 2
                FileGet(1, prec, Val(TextBox1.Text))
                prec.korsi(J) = z
                s = Val(TextBox1.Text)
                FilePut(1, prec, s)
                clear2()
            ElseIf X = 2 Then


                Exit Sub
            End If
        ElseIf z = 2 Then
            If lang = True Then
                MsgBox("This seat is already booked and confirmed !")
            Else
                MsgBox("هذا المقعد بالفعل محجوز وتم تأكيده !")
            End If

        End If

    End Sub

    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        If z = 0 Then
            If lang = True Then
                MsgBox("This seat is already free to be booked !")
            Else
                MsgBox("هذا المقعد بالفعل متاح ليتم حجزه !")
            End If

        ElseIf z = 1 Then
            If lang = True Then
                X = MsgBox("Do you want to cancel booking this seat ?!", MsgBoxStyle.OkCancel)
            Else
                X = MsgBox("هل تريد إلغاء حجز هذا المقعد ؟!", MsgBoxStyle.OkCancel)
            End If

            If X = 1 Then
                buttons(J).BackgroundImage = My.Resources.ChairSelected
                FileGet(1, prec, Val(TextBox1.Text))
                z = 0
                prec.korsi(J) = 0
                prec.passenger(J) = "0"
                prec.phone(J) = "0"
                prec.address(J) = "0"
                s = Val(TextBox1.Text)
                FilePut(1, prec, s)
                clear2()
            ElseIf X = 2 Then

                Exit Sub
            End If
        ElseIf z = 2 Then
            If lang = True Then
                MsgBox("Sorry, already confirmed, cannot be canceled !")
            Else
                MsgBox("نأسف, تم التأكيد بالفعل, لا يمكن الإلغاء !")
            End If

        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = Date.Now.ToString("   dddd d MMMM yyyy")
        Label4.Text = Date.Now.ToString("              h:mm:ss  tt")
    End Sub

    Private Sub Button68_Click(sender As Object, e As EventArgs) Handles Button68.Click
        lang = True
        Label2.Text = "EgyAir Reservations"
        Label3.Text = "Trip No."
        Label5.Text = "Date"
        Label6.Text = "Travel From"
        Label7.Text = "Time"
        Label8.Text = "Travel Date"
        Label9.Text = "Arrive To"
        Label10.Text = "Passenger"
        Label11.Text = "Phone"
        Label12.Text = "Address"
        Label13.Text = "Class"
        GroupBox1.Text = "Passenger Info"
        Button63.Text = "New Trip"
        Button64.Text = "Booking"
        Button65.Text = "Confirm Booking"
        Button66.Text = "Cancel Booking"
        Button67.Text = "Exit"
        Me.Text = "EgyAir"
        If TextBox10.Text = "درجة أولى" Then
            TextBox10.Text = "First Class"
        ElseIf TextBox10.Text = "درجة ثانية" Then
            TextBox10.Text = "Second Class"
        ElseIf TextBox10.Text = "درجة إقتصادية" Then
            TextBox10.Text = "Economy Class"
        End If
        PictureBox1.BackgroundImage = My.Resources.Plane1Edit3



    End Sub

    Private Sub Button69_Click(sender As Object, e As EventArgs) Handles Button69.Click
        lang = False
        Label2.Text = "حجوزات إيجي إير"
        Label3.Text = "رقم الرحلة"
        Label5.Text = "التاريخ"
        Label6.Text = "السفر من"
        Label7.Text = "الوقت"
        Label8.Text = "تاريخ الرحلة"
        Label9.Text = "الوصول إلى"
        Label10.Text = "المسافر"
        Label11.Text = "رقم الهاتف"
        Label12.Text = "العنوان"
        Label13.Text = "الدرجة"
        GroupBox1.Text = "معلومات المسافر"
        Button63.Text = "رحلة جديدة"
        Button64.Text = "الحجز"
        Button65.Text = "تأكيد الحجز"
        Button66.Text = "إلغاء الحجز"
        Button67.Text = "خروج"
        Me.Text = "إيجي إير"
        If TextBox10.Text = "First Class" Then
            TextBox10.Text = "درجة أولى"
        ElseIf TextBox10.Text = "Second Class" Then
            TextBox10.Text = "درجة ثانية"
        ElseIf TextBox10.Text = "Economy Class" Then
            TextBox10.Text = "درجة إقتصادية"
        End If
        PictureBox1.BackgroundImage = My.Resources.Plane1EditAR



    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Enabled = True
        Dim filePath As String = IO.Path.Combine(Application.StartupPath, "Raslan.txt")
        FileOpen(1, filePath, OpenMode.Random, , , Len(prec))
        prec.korsi = New Integer(59) {}
        prec.passenger = New String(59) {}
        prec.phone = New String(59) {}
        prec.address = New String(59) {}
        For i = 1 To 60
            buttons(i - 1) = Me.Controls("Button" & i + 2)
            AddHandler buttons(i - 1).Click, AddressOf korsyy_no
        Next
        AddHandler Button1.Click, AddressOf captain
        AddHandler Button2.Click, AddressOf captain
    End Sub

    Sub view()
        FileGet(1, prec, Val(TextBox1.Text))
        If prec.trip_no <> Val(TextBox1.Text) Then
            If lang = True Then
                MsgBox("There is no trip associated to this number !")
            Else
                MsgBox("يوجد بالفعل رحلة تحمل هذا الرقم !")
            End If

            FileClose(1)
            Exit Sub
        End If
        TextBox2.Text = prec.trip_time
            TextBox3.Text = prec.trip_date
            TextBox4.Text = prec.arrive_time
            TextBox5.Text = prec.city
            TextBox6.Text = prec.city1
            For i = 0 To 59
                If prec.korsi(i) = 1 Then
                buttons(i).BackgroundImage = My.Resources.ChairChosen
            ElseIf prec.korsi(i) = 2 Then
                buttons(i).BackgroundImage = My.Resources.ChairTaken
            Else
                buttons(i).BackgroundImage = My.Resources.Chair
            End If
            Next

    End Sub
    Public Sub korsyy_no(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBox1.Text = "" Then
            If lang = True Then
                MsgBox("Enter a number first in the trip number field !")
            Else
                MsgBox("ادخل رقم أولا في خانة رقم الرحلة !")
            End If

        Else
            FileGet(1, prec, Val(TextBox1.Text))
            For i = 0 To buttons.Length - 1
                If buttons(i) Is sender Then
                    J = i
                    Exit For
                End If

            Next
            z = prec.korsi(J)
            If z = 0 Then
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox9.Text = ""
            Else
                TextBox7.Text = prec.passenger(J)
                TextBox8.Text = prec.phone(J)
                TextBox9.Text = prec.address(J)
            End If
            If lang = True Then
                If J < 16 Then
                    TextBox10.Text = "First Class"
                ElseIf J >= 16 And J < 36 Then
                    TextBox10.Text = "Second Class"
                ElseIf J >= 36 Then
                    TextBox10.Text = "Economy Class"
                End If
            Else
                If J < 16 Then
                    TextBox10.Text = "درجة أولى"
                ElseIf J >= 16 And J < 36 Then
                    TextBox10.Text = "درجة ثانية"
                ElseIf J >= 36 Then
                    TextBox10.Text = "درجة إقتصادية"
                End If
            End If



            For i = 0 To buttons.Length - 1
                If prec.korsi(i) = 1 Then
                    buttons(i).BackgroundImage = My.Resources.ChairChosen
                ElseIf prec.korsi(i) = 2 Then
                    buttons(i).BackgroundImage = My.Resources.ChairTaken
                Else
                    buttons(i).BackgroundImage = My.Resources.Chair
                End If
            Next
            buttons(J).BackgroundImage = My.Resources.ChairSelected
        End If
    End Sub
    Sub captain()
        If lang = True Then
            MsgBox("Captain's seat is - and will always be - reserved :)")
        Else
            MsgBox("مقعد الكابتن محجوز وسيظل محجوز :)")
        End If

    End Sub
    Sub clear()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
    End Sub
    Sub clear2()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox9.Clear()
        TextBox10.Clear()
    End Sub

End Class
