import { getIdParam, renderDetails } from "../../control-modules/site.js";
import { getData } from "../../data/repository.js";

const id = getIdParam();

async function loadPage() {
    await loadTeamSeasonDetails(id);
}

async function loadTeamSeasonDetails(id) {
    let teamSeason = await getData(`TeamSeasons/${id}`);
    renderDetails("#team-season-details", teamSeason, "#team-season");
}

loadPage();
