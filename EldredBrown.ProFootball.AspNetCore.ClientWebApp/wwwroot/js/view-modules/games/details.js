import { getIdParam } from "../../control-modules/site.js";
import { loadGameDetails } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadGameDetails(id);
}

loadPage();
