import { getData } from "../../data/repository.js";

async function loadLeagues() {
    let leagues = await getData("Leagues");
    if (leagues) {
        renderLeagues(leagues);
    }
}

async function loadPage() {
    await loadLeagues();
}

function renderLeagues(data) {
    $("#leagues-body tr").remove();

    let template = $("#league-row").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "leagues": data
    };

    let html = templateScript(context);
    $("#leagues-body").append(html);
}

loadPage();
