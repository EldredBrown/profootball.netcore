import { getIdParam } from "../../control-modules/site.js";
import { deleteData } from "../../data/repository.js";
import { loadTeamSeasonDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadTeamSeasonDetails(id);
}

$("form").submit(function (e) {
    e.preventDefault();
    deleteData(`TeamSeasons/${id}`);
    window.location.pathname = "/TeamSeasonsAdmin/Index";
});

loadPage();
