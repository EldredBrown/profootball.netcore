import { loadSeasons } from "../../control-modules/site.js";
import { getData } from "../../data/repository.js";

let selectedSeasonYear = 1920;
let teamSeasonsCache = null;

async function loadPage() {
    await loadSeasons(selectedSeasonYear);
    await loadTeamSeasons();
}

async function loadTeamSeasons() {
    if (!teamSeasonsCache) {
        teamSeasonsCache = await getData("TeamSeasons");
    }

    if (teamSeasonsCache) {
        let teamSeasons = teamSeasonsCache.filter(ts => ts.seasonYear === selectedSeasonYear);
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

async function runWeeklyUpdate() {
    const url = `${api}/Services/RunWeeklyUpdate/${selectedSeasonYear}`;

    await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        }
    }).then(async response => {
        console.log(await response);
    }).catch(async error => {
        console.log(await error);
    });
}

$("#run-weekly-update").click(function (e) {
    e.preventDefault();

    runWeeklyUpdate();
});

$("#season").change(function (e) {
    e.preventDefault();
    selectedSeasonYear = parseInt($(this).val());
    loadTeamSeasons();
});

loadPage();
