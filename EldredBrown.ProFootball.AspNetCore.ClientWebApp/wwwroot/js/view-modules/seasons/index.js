import { getData } from "../../data/repository.js";

async function loadPage() {
    await loadSeasons();
}

async function loadSeasons() {
    let seasons = await getData("Seasons");
    if (seasons) {
        renderSeasons(seasons);
    }
}

function renderSeasons(data) {
    $("#seasons-body tr").remove();

    let template = $("#season-row").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "seasons": data
    };

    let html = templateScript(context);
    $("#seasons-body").append(html);
}

loadPage();
