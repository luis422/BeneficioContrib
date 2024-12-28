// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    AplicarMascarasCampos();
    CustomizarValidacao();
});

function AplicarMascarasCampos() {

    // qualquer elemento que tenha o atributo 'mask'
    var elements = $("[mask]");
    for (var i = 0; i < elements.length; i++) {
        $(elements[i]).attr('data-inputmask', $(elements[i]).attr('mask'));
        $(elements[i]).removeAttr('mask');
        //console.log(elements[i]);
    }

    $("[data-inputmask]").inputmask();
}

function CustomizarValidacao() {

    if ($.validator === undefined || $.validator.methods === undefined) {
        return;
    }

    //------[ Validação Customizada ]---------

    // Número com ',' em vez de '.' na validação de intervalo
    $.validator.methods.range = function (value, element, param) {
        console.log(globalizedValue);
        var globalizedValue = value.trimEnd(',').replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    }

    // Número com ',' em vez de '.' na validação de número
    $.validator.methods.number = function (value, element) {
        return this.optional(element) || /-?(?:\d+|\d{1,3}(?:[\s\.,]\d{3})+)(?:[\.,]\d+)?$/.test(value);
    }

    //Date dd/MM/yyyy
    $.validator.methods.date = function (value, element) {
        var date = value.split("/");
        return this.optional(element) || !/Invalid|NaN/.test(new Date(date[2], date[1], date[0]).toString());
    }

    //----------------------------------------
}

function ExibirModal(
    linkPagina,
    larguraModal = 710,
    tituloModal = "",
    texto = "",
    exibirIconeXFechar = true,
    textoBotaoFechar = "Fechar",
    exibirBotaoCancelar = false,
    textoBotaoCancelar = "Cancelar",
    exibirBotaoConfirmar = false,
    textoBotaoConfirmar = "OK",
    callbackAntesAbrir = undefined,
    callbackDepoisAbrir = undefined,
    callbackAntesFechar = undefined,
    callbackDepoisFechar = undefined,
) {
    // Realiza uma chamada AJAX para obter o HTML da página de detalhes
    fetch(linkPagina)
        .then(response => response.text())
        .then(html => {
            // Exibe o HTML retornado no modal do SweetAlert2
            Swal.fire({
                title: tituloModal,
                text: texto,
                html: html,
                width: larguraModal,
                showCloseButton: exibirIconeXFechar,
                textCloseButton: textoBotaoFechar,
                textCancelButton: textoBotaoCancelar,
                showCancelButton: exibirBotaoCancelar,
                focusConfirm: false,
                confirmButtonText: textoBotaoConfirmar,
                showConfirmButton: exibirBotaoConfirmar,
                willOpen: () => {
                    if (callbackAntesAbrir !== undefined) {
                        callbackAntesAbrir();
                    }
                },
                didOpen: () => {
                    if (callbackDepoisAbrir !== undefined) {
                        callbackDepoisAbrir();
                    }
                },
                willClose: () => {
                    if (callbackAntesFechar !== undefined) {
                        callbackAntesFechar();
                    }
                },
                didClose: () => {
                    if (callbackDepoisFechar !== undefined) {
                        callbackDepoisFechar();
                    }
                }
            });
        })
        .catch(error => {
            console.error('Erro ao carregar página:', error);
            Swal.fire('Erro', 'Não foi possível carregar a página.', 'error');
        });
}