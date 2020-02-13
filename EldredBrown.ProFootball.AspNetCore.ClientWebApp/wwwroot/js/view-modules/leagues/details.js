import { getIdParam } from "../../control-modules/site.js";
import { loadLeagueDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadLeagueDetails(id);
}

loadPage();
