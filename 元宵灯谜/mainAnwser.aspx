<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mainAnwser.aspx.cs" Inherits="yuanxiao.mainAnwser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            第<span id="Pnum" runat="server"></span>题
        </div>
        <div>
            <div><span id="Rquestion" runat="server"></span></div>
            <div>
                <asp:RadioButton ID="AnswerA" runat="server" GroupName="Answer"/><br />
                <asp:RadioButton ID="AnswerB" runat="server" GroupName="Answer"/><br />
                <asp:RadioButton ID="AnswerC" runat="server" GroupName="Answer"/><br />
                <asp:RadioButton ID="AnswerD" runat="server" GroupName="Answer"/><br />

            </div>
        <div>
            <asp:Button ID="next" runat="server" OnClick="next_Click" Text="我是个按钮" />
        </div>
        <div><span id="timecost" runat="server"></span></div>
        <%--timecost用于计时，到时候传参也是传这里面值--%>
        </div>
    </form>
</body>
</html>
