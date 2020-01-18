import { getIdParam } from "../../control-modules/site.js";
import { getData, putData } from "../../data/repository.js";
import { loadPartial, validateInput } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadPartial();
    await loadTeamSeasonDetails();
}

async function loadTeamSeasonDetails() {
    let teamSeason = await getData(`TeamSeasons/${id}`);
    renderTeamSeasonDetails(teamSeason);
}

function renderTeamSeasonDetails(teamSeason) {
    $("#team-name").val(teamSeason.teamName);
    $("#season").val(teamSeason.seasonYear);
    $("#league-name").val(teamSeason.leagueName);
}

async function updateTeamSeason() {
    let teamSeason = validateInput(id);

    if (!teamSeason) {
        return;
    }

    await putData(`TeamSeasons/${id}`, teamSeason);

    return teamSeason;
}

$("form").submit(async function (e) {
    e.preventDefault();

    let teamSeason = await updateTeamSeason();
    if (teamSeason) {
        window.location.pathname = "TeamSeasonsAdmin/Index";
    }
});

loadPage();
