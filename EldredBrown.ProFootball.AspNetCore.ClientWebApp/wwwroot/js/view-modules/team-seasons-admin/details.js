import { getIdParam } from "../../control-modules/site.js";
import { loadTeamSeasonDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadTeamSeasonDetails(id);
}

loadPage();
