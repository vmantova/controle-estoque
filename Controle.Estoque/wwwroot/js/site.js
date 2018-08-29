// Write your JavaScript code.

//jquery
$(document).ready(function () {

    $("#estoqueAtual").hide()

    $(".fone-fixo").mask('(99) 9999-9999');

    $(".campo-data").mask('99/99/9999');

    $(".campo-numerico").mask("999999999");

    $(document).on('keydown', '[data-mask-for-cpf-cnpj]', function (e) {

        var digit = e.key.replace(/\D/g, '');

        var value = $(this).val().replace(/\D/g, '');

        var size = value.concat(digit).length;

        $(this).mask((size <= 11) ? '000.000.000-00' : '00.000.000/0000-00');
    });

    $("#btnSearch").click(function () {

        var param = $("#searchParam").val();

        location.href = '/Cliente/Index?searchParam=' + param;

    });

    $("#btnSearchProd").click(function () {

        var paramProd = $("#searchProd").val();

        location.href = '/Estoque/Index?searchProd=' + paramProd;
    });

    $("#btnSearchVendas").click(function () {

        var paramVenda = $("#SearchVenda").val();

        location.href = 'Vendas/Index?searchVenda=' + paramVenda;

    });

    $("#btnSearchLoc").click(function () {

        var param = $("#searchLoc").val();

        location.href = 'Locacao/Index?searchLoc=' + param;
    });

    $("#ProdutoId").change(function () {

        var id = $(this).val();

        $.get('/Locacao/GetEstoqueAtual', { id: id }, function (data) {

            $("#estoqueAtual").html('<span style="font-size:15px;" class="label label-info">Estoque Atual: ' + data + '</span>');
            $('#estoqueAtual').show();

        })

    });
});

function CalculaPreco(url) {
   
    var qtde = $("#Quantidade").val();

    var id = $("#ProdutoId").val();

    $.get(url, {
        id: id,
        quantidade: qtde
    }, function (data) {
        $("#ValorTotal").val(data.toString());
    });
}