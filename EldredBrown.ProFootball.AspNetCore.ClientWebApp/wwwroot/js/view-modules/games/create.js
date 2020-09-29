import { GamesControl } from "../../control-modules/games";
import { postData } from "../../data/repository";
import { loadPartial, validateInput } from "./shared";

async function createGame() {
    let game = validateInput();

    if (!game) {
        return;
    }

    await postData("Games", game);

    return game;
}

async function loadPage() {
    GamesControl.getCookieValues();

    await loadPartial();
    selectWeek();
}

function selectWeek() {
    if (GamesControl.selectedWeek) {
        $("#week").val(GamesControl.selectedWeek);
    } else {
        $("#week").val(1);
    }
}

$("form").submit(async function (e) {
    e.preventDefault();

    let game = await createGame();
    if (game) {
        GamesControl.setSelectedSeasonYear(game.seasonYear);
        GamesControl.setSelectedWeek(game.week);

        window.location.pathname = "Games/Create";
    }
});

loadPage();
