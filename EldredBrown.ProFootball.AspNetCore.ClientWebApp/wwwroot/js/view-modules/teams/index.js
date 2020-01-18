import { getData } from "../../data/repository.js";

async function loadPage() {
    await loadTeams();
}

async function loadTeams() {
    let teams = await getData("Teams");
    if (teams) {
        renderTeams(teams);
    }
}

function renderTeams(data) {
    $("#teams-body tr").remove();

    let template = $("#team-row").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "teams": data
    };

    let html = templateScript(context);
    $("#teams-body").append(html);
}

loadPage();
