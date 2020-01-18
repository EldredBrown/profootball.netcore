import { getData } from "../../data/repository.js";

async function loadPage() {
    await loadTeamSeasons();
}

async function loadTeamSeasons() {
    let teamSeasons = await getData("TeamSeasons");
    if (teamSeasons) {
        renderTeamSeasons(teamSeasons);
    }
}

function renderTeamSeasons(data) {
    $("#team-seasons-body tr").remove();

    let template = $("#team-season-row").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "teamSeasons": data
    };

    let html = templateScript(context);
    $("#team-seasons-body").append(html);
}

loadPage();
