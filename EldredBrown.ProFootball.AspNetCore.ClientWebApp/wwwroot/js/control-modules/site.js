import { getData } from "../data/repository.js";

const emptyStringErrorMessage = "The value '' is invalid.";

let seasons = null;

function getIdParam() {
    let queryString = window.location.pathname;
    let params = queryString.split("/");
    let id = params[3];

    return parseInt(id);
}

async function loadSeasons(selectedSeasonYear = null) {
    seasons = await getData("Seasons");
    if (seasons) {
        renderSeasons(seasons, selectedSeasonYear);
    }
}

function renderDetails(templateId, data, tagId) {
    let template = $(templateId).html();
    let templateScript = Handlebars.compile(template);
    let html = templateScript(data);
    $(tagId).append(html);
}

function renderSeasons(data, selectedSeasonYear = null) {
    let template = $("#season-year").html();
    let templateScript = Handlebars.compile(template);

    let context = {
        "seasons": data
    };

    let html = templateScript(context);
    $("#season").append(html);

    if (selectedSeasonYear) {
        $("#season").val(selectedSeasonYear);
    }
}

export { emptyStringErrorMessage, getIdParam, loadSeasons, renderDetails, seasons };
