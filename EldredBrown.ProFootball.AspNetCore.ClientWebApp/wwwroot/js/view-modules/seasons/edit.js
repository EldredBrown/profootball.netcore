import { getIdParam } from "../../control-modules/site.js";
import { getData, putData } from "../../data/repository.js";
import { validateInput } from "./shared.js";

const id = getIdParam();

async function loadPage() {
    await loadSeasonDetails(id, renderSeasonDetails);
}

async function loadSeasonDetails() {
    let season = await getData(`Seasons/${id}`);
    renderSeasonDetails(season);
}

function renderSeasonDetails(season) {
    $("#year").val(season.year);
    $("#weeks-scheduled").val(season.numOfWeeksScheduled);
    $("#weeks-completed").val(season.numOfWeeksCompleted);
    $("#weeks-completed").prop("max", season.numOfWeeksScheduled);
}

async function updateSeason() {
    let season = validateInput(id);

    if (!season) {
        return;
    }

    await putData(`Seasons/${id}`, season);

    return season;
}

$("form").submit(async function (e) {
    e.preventDefault();

    let season = await updateSeason();
    if (season) {
        window.location.pathname = "Seasons/Index";
    }
});

loadPage();
