"use strict";

// Inicializa a conexão com o SignalR Hub
const conexao = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44304/messageHub", signalR.HttpTransportType.WebSockets)
    .build();

console.log("Iniciando tentativa de conexão com o SignalR...");

// Função para iniciar a conexão com o Hub
function iniciarConexao() {
    conexao.start()
        .then(aoConectarComSucesso)
        .catch(aoConectarComErro);
}

// Callback de sucesso ao iniciar a conexão
function aoConectarComSucesso() {
    console.log("Conexão iniciada com sucesso.");
    document.getElementById("connectionStatus").textContent = "Conexão iniciada com sucesso.";
    console.log("Connection ID: ", conexao.connectionId);

    // Escuta o evento de mensagens do servidor
    conexao.on("ReceiveMessageGetAll", aoReceberMensagem);
}

// Callback de erro ao iniciar a conexão
function aoConectarComErro(err) {
    console.error("Erro ao iniciar a conexão: ", err.toString());
    document.getElementById("connectionStatus").textContent = "Erro na conexão: " + err.toString();
}

// Função chamada ao receber uma mensagem do servidor
function aoReceberMensagem(versionInfo) {
    console.log('Dados recebidos do servidor: ', versionInfo);

    // Limpa a lista antes de adicionar novos elementos
    limparListaSistemas();

    // Faz o parse dos dados recebidos (JSON)
    const informacoesVersao = fazerParseInformacoesVersao(versionInfo);
    console.log('Dados parseados: ', informacoesVersao);

    // Renderiza o card com as informações recebidas
    renderizarCard(informacoesVersao);
}

// Limpa o conteúdo da lista de sistemas
function limparListaSistemas() {
    document.getElementById("sistemasList").innerHTML = '';
    console.log("Lista de sistemas limpa.");
}

// Faz o parse das informações da versão
function fazerParseInformacoesVersao(versionInfo) {
    try {
        return JSON.parse(versionInfo);
    } catch (e) {
        console.error("Erro ao fazer o parse das informações da versão: ", e);
        return null;
    }
}

// Renderiza o card com as informações da versão
function renderizarCard(informacoesVersao) {
    if (!informacoesVersao) {
        console.warn("Dados da versão não disponíveis para renderização.");
        return;
    }

    const card = document.createElement("div");
    card.className = "card";

    const conteudoCard = document.createElement("div");
    conteudoCard.className = "card-content";

    // Criação dos elementos do card
    const elementoVersaoApi = criarElementoCard("Versão da API: ", informacoesVersao.ApiVersion);
    const elementoAmbiente = criarElementoCard("Ambiente: ", informacoesVersao.Environment);
    const elementoTimestamp = criarElementoCard("Data/Hora: ", informacoesVersao.Timestamp);
    const elementoUsuario = criarElementoCard("Usuário: ", informacoesVersao.User);

    // Adiciona os elementos ao conteúdo do card
    conteudoCard.appendChild(elementoVersaoApi);
    conteudoCard.appendChild(elementoAmbiente);
    conteudoCard.appendChild(elementoTimestamp);
    conteudoCard.appendChild(elementoUsuario);

    card.appendChild(conteudoCard);
    document.getElementById("sistemasList").appendChild(card);
    console.log("Card renderizado com sucesso.");
}

// Cria um elemento de card com um rótulo e valor
function criarElementoCard(rotulo, valor) {
    const elemento = document.createElement("div");
    elemento.textContent = rotulo + valor;
    console.log(`${rotulo}${valor}`);
    return elemento;
}

// Inicia a conexão com o SignalR Hub
iniciarConexao();