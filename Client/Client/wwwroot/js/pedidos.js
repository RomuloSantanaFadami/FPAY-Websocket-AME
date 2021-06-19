"use strict";

var app;

class App {

    constructor() {
        this.conexao = null;
        this.urlBaseWebApi = '';
        // 'https://localhost:44322';
    }

    init() {
        this.addEvent();
    }

    addEvent() {
        $("#btnConectar").on("click", () => {
            if (!this.conexao || !this.conexao.connectionStarted)
                this.conectar()
            else
                this.desconectar();
        });


        $("#btnPedido").on("click", () => {
            var param = {
                "title": $("#ddlPraca").val(),
                "description": "CAT 01",
                "amount": parseFloat($("#txtValor").val()) * 100,
                "type": "PAYMENT",
                "currency": "BRL",
                "attributes": {
                    "transactionChangedCallbackUrl": null,
                    "items": [
                        {
                            "ean": null,
                            "sku": "None",
                            "amount": parseFloat($("#txtValor").val()) * 100,
                            "quantity": 1,
                            "description": "CAT 01"
                        }
                    ],
                    "customPayload": {
                        "isFrom": $("#txtSLM").val()
                    },
                    "paymentOnce": true,
                    "riskHubProvider": "SYNC",
                    "origin": "PDV"
                }
            };
            fetch(`${app.urlBaseWebApi}/pedidos/CreateOrder`, {
                headers: {
                    'Accept': "application/json, text/plain, */*",
                    'Content-Type': "application/json;charset=utf-8"
                },
                method: "POST",
                body: JSON.stringify(param)
            })
                .then(
                    response => response.json()
                )
                .then(response => {
                    console.log("retorno: ", response);
                    $("#qrcode").html('');
                    var qrcode = new QRCode("qrcode", {
                        text: response.qrCodeLink,
                        width: 350,
                        height: 350,
                        colorDark: "#000000",
                        colorLight: "#ffffff",
                        correctLevel: QRCode.CorrectLevel.H
                    });
                    qrcode.makeCode(response.qrCodeLink);
                });
                
        });
    }

    // everntos
    conectar() {
        this.urlBaseWebApi = $("#txtUrl").val();
        if (!this.conexao) {
            let slm = JSON.stringify({ id: $("#txtSLM").val() });
            this.conexao = new signalR.HubConnectionBuilder()
                .withUrl(`${this.urlBaseWebApi}/pedidosHub?slm=${slm}`)
                .withAutomaticReconnect()
                .build();

            this.conexao.onclose(() => {
                alert("descsjkdjf");
                this.conectar();
            });

            this.conexao.on("ReceiveCreateOrderCallback", (callback) => {
                alert("PAGO");
            })
        }

        this.conexao.start().then(function () {
            $("#btnConectar").html("Desconectar");
            $("#btnConectar").removeClass("btn-success");
            $("#btnConectar").addClass("btn-danger");
            $("#divPedido").prop("hidden", !app.conexao.connectionStarted);
        }).catch(function (err) {
            return console.error(err.toString());
        });
    }

    desconectar() {
        if (this.conexao) {
            this.conexao.stop();
            $("#btnConectar").html("Conectar");
            $("#btnConectar").addClass("btn-success");
            $("#btnConectar").removeClass("btn-danger");
            $("#divPedido").prop("hidden", true);
        }
    }

}


$(document).ready(() => {
    app = new App();
    app.init();
});