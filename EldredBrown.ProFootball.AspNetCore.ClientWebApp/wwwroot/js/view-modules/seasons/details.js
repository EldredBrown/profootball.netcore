import { getIdParam } from "../../control-modules/site.js";
import { loadSeasonDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadSeasonDetails(id);
}

loadPage();
