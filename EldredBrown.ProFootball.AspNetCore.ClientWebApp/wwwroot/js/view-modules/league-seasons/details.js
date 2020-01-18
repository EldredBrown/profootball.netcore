import { getIdParam } from "../../control-modules/site.js";
import { loadLeagueSeasonDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadLeagueSeasonDetails(id);
}

loadPage();
