<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SenaTest._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />
    <br />


    
        <div class="row">
            <div class="col-md-4">
                <div class="form-group">
                    <label class="control-label col-md-3">Mal Kodu veya Mal Adı</label>
                    <div class="col-md-9">
                        <input type="text" class="form-control" placeholder="..." id="txtOrderID" runat="server">
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="firstDate">Başlangıç:</label>
                    <input type="date" id="txtfirstDate" name="birthday" runat="server">
                </div>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label for="lastDate">Bitiş:</label>
                    <input type="date" id="txtlastDate" name="birthday" runat="server">
                </div>
            </div>
            <button type="button" class="btn btn-warning mr-10" id="BtnStockStatus" runat="server" onserverclick="BtnStockStatus_ServerClick">Kayıt Ara</button>
                        <button type="button" class="btn btn-success mr-10" id="BtnExcel" runat="server" onserverclick="BtnExcel_ServerClick" >Excel'e Aktar</button>

        </div>

  
    <br />
    <br />

        <div class="row" style="font-size: small !important;">
            <div class="col-md-12">
                <div class="panel panel-default card-view">
                    <div class="panel-wrapper collapse in">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-wrap">
                                        <div class="form-body">
                                            <h6 class="txt-dark capitalize-font"><i class="ti-view-list-alt mr-10"></i></h6>
                                            <hr class="light-grey-hr" />
                                            <div class="panel-wrapper collapse in">
                                                <div class="panel-body">
                                                    <div class="table-wrap" style="margin-top: -40px;">
                                                        <div class="">
                                                            <table id="dtTable" class="table table-hover display">
                                                                <thead>
                                                                    <tr>
                                                                        <th>Sıea No</th>
                                                                        <th>İşlem Tur</th>
                                                                        <th>EvrakNo</th>
                                                                        <th>Tarih</th>
                                                                        <th>Giris Miktarı</th>
                                                                        <th>Çıkış Miktarı</th>
                                                                        <th>Stok Miktar</th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Panel runat="server" ID="pnlStock_List"></asp:Panel>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
