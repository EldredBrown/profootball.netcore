import { getData } from "../../data/repository.js";

async function loadLeagueSeasons() {
    let leagueSeasons = await getData("LeagueSeasons");
    if (leagueSeasons) {
        renderLeagueSeasons(leagueSeasons);
    }
}

async function loadPage() {
    await loadLeagueSeasons();
}

function renderLeagueSeasons(data) {
    $("#league-seasons-body tr").remove();

    let template = $("#league-season-row").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "leagueSeasons": data
    };

    let html = templateScript(context);
    $("#league-seasons-body").append(html);
}

loadPage();
