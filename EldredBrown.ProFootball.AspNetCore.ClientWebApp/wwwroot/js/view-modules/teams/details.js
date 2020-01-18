import { getIdParam } from "../../control-modules/site.js";
import { loadTeamDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadTeamDetails(id);
}

loadPage();
