import { emptyStringErrorMessage, loadSeasons, renderDetails } from "../../control-modules/site.js";
import { getData } from "../../data/repository.js";
import { TeamSeason } from "../../data/models/team-season.js";

async function loadLeagues() {
    let leagues = await getData("Leagues");
    if (leagues) {
        renderLeagues(leagues);
    }
}

async function loadPartial() {
    await loadSeasons();
    await loadLeagues();
}

async function loadTeamSeasonDetails(id) {
    let teamSeason = await getData(`TeamSeasons/${id}`);
    renderDetails("#team-season-details", teamSeason, "#team-season");
}

function renderLeagues(data) {
    let template = $("#league-name").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "leagues": data
    };

    let html = templateScript(context);
    $("#league").append(html);
}

function validateInput(teamSeasonId = 0) {
    let inputValid = true;

    $("#validation-summary li").remove();

    let teamName = $("#team-name").val();
    if (teamName) {
        $("#validation-for-team-name").text("");
    } else {        
        $("#validation-for-team-name").text(emptyStringErrorMessage);
        $("#validation-summary").append("<li>Please enter a team name.</li>");
        inputValid = false;
    }

    let seasonYear = parseInt($("#season").val());
    if (seasonYear) {
        $("#validation-for-season-year").text("");
    } else {
        $("#validation-for-season-year").text(emptyStringErrorMessage);
        $("#validation-summary").append("<li>Please enter a year.</li>");
        inputValid = false;
    }

    let leagueName = $("#league").val();
    if (leagueName) {
        $("#validation-for-league-name").text("");
    } else {
        $("#validation-for-league-name").text(emptyStringErrorMessage);
        $("#validation-summary").append("<li>Please enter a league name.</li>");
        inputValid = false;
    }

    if (!inputValid) {
        return null;
    }

    return new TeamSeason(teamSeasonId, teamName, seasonYear, leagueName);
}

export { loadPartial, loadTeamSeasonDetails, validateInput };
