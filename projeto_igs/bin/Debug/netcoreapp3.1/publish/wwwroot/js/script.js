/*
document.getElementById("sched_tel").addEventListener("input", function() {
    var telInput = document.getElementById("sched_tel");
    var telValue =  telInput.value.replace(/[^0-9]/g, ''); // Remove não números
    var formattedValue = '(';

    for (var i = 0; i < telValue.length; i++) {
        formattedValue += telValue[i];
        if (i === 1) {
            formattedValue += ')';
        }
        if (i === 6) {
            formattedValue += '-';
        }
    }

    telInput.value = formattedValue;
});
*/

$(document).ready(function()
{
    $("#script_addsolicit").submit(function(e) {
        e.preventDefault();
        Cadastrar();
    });
})

function Cadastrar() 
{
    var codSol = document.getElementById("solic1").value;
    var textSol = document.getElementById("solic2").value;
    var errors = "";

    if (codSol.trim() === "")
    {
        errors = errors + "Código do produto.\n";
    }

    if (textSol.trim() === "")
    {
        errors = errors + "Descrição e informações adicionais.\n";
    }
    if (errors != "")
    {
        alert("Existem campos obrigatórios que não foram preenchidos:\n\n" + errors);
        e.preventDefault();
    }

    let parametros = {
        Codigo: codSol,
        txtBox: textSol
    };

    $.post("/Contato/CadastrarSol", parametros)

    .done(function(data)
    {
        if (data.status == "OK")
        {
            $("#script_addsolicit").after("<div class='modal_container' id='asyncModal'><div class='modal'><h1>Solicitação enviada!</h1><p>Recebemos sua mensagem e entraremos em contato em breve pelo e-mail fornecido. Agradecemos a preferência!</p><br><br><br><p class='modal-footer'><button type='button' class='modal-close-btn dark-blue-innershadow' id='asyncBtn'>FECHAR</button></p></div></div>");

            var modal = document.getElementById("asyncModal");
            var closeModal = document.getElementById("asyncBtn");

            closeModal.addEventListener("click", function () {
                modal.classList.add("modalOff");
            });
        }
        else
        {
            alert(data.mensagem);
        }
    })

    .fail(function(data) 
    {
        alert(data.mensagem);
    })
};

/* ======================================================================================= */

$(document).ready(function()
{
    $("#script_addprod").submit(function(e) {
        e.preventDefault();
        CadastrarP();
    });
})

function CadastrarP()
{
    var codProd = document.getElementById("addp1").value;
    var nomeProd = document.getElementById("addp2").value;
    var descProd = document.getElementById("addp3").value;
    var precoProd = document.getElementById("addp4").value;
    var errors = "";

    if (codProd.trim() === "")
    {
        errors = errors + "Código do produto.\n";
    }

    if (nomeProd.trim() === "")
    {
        errors = errors + "Nome do produto.\n";
    }

    if (descProd.trim() === "")
    {
        errors = errors + "Descrição do produto.\n";
    }

    if (precoProd.trim() === "")
    {
        errors = errors + "Preço do produto.\n";
    }

    if (errors != "")
    {
        alert("Existem campos obrigatórios que não foram preenchidos:\n\n" + errors)
        e.preventDefault();
    }

    let parametros1 = {
        Codigo: codProd,
        NomePrd: nomeProd,
        DescPrd: descProd,
        Preco: precoProd
    };

    $.post("/Produto/AddProduto", parametros1)

    .done(function(data)
    {
        if (data.status === "OK")
        {
            $("#script_addprod").after("<div class='modal_container' id='asyncModal1'><div class='modal'><h1>Produto cadastrado!<br><br><br><p class='modal-footer'><button type='button' class='modal-close-btn dark-blue-innershadow' id='asyncBtn1'>FECHAR</button></p></div></div>");

            var modal = document.getElementById("asyncModal1");
            var closeModal = document.getElementById("asyncBtn1");

            closeModal.addEventListener("click", function () {
                modal.classList.add("modalOff");
            });
        }
        else
        {
            alert(data.mensagem);
        }
    })

    .fail(function(data) 
    {
        alert(data.mensagem);
    })
};