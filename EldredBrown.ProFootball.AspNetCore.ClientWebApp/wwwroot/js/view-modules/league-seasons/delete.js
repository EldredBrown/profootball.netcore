import { getIdParam } from "../../control-modules/site.js";
import { deleteData } from "../../data/repository.js";
import { loadLeagueSeasonDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadLeagueSeasonDetails(id);
}

$("form").submit(function (e) {
    e.preventDefault();
    deleteData(`LeagueSeasons/${id}`);
    window.location.pathname = "/LeagueSeasons/Index";
});

loadPage();
