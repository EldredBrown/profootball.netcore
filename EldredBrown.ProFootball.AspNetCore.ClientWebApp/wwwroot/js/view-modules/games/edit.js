﻿import { getIdParam } from "../../control-modules/site.js";
import { GamesControl } from "../../control-modules/games.js";
import { getData } from "../../data/repository.js";
import { loadPartial, validateInput } from "./shared.js";

const id = getIdParam();

let oldGame = null;

async function loadGameDetails() {
    let game = await getData(`Games/${id}`);
    renderGameDetails(game);

    oldGame = game;
}

async function loadPage() {
    await loadPartial();
    await loadGameDetails(renderGameDetails);
}

async function putGame(game) {
    const url = `${api}/Games/${id}`;

    await fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            "oldGame": oldGame,
            "game": game
        })
    }).then(async response => {
        console.log(await response);
    }).catch(async error => {
        console.log(await error);
    });
}

function renderGameDetails(game) {
    $("#season").val(game.seasonYear);
    $("#week").val(game.week);
    $("#guest-name").val(game.guestName);
    $("#guest-score").val(game.guestScore);
    $("#host-name").val(game.hostName);
    $("#host-score").val(game.hostScore);
    $("#is-playoff-game")[0].checked = game.isPlayoffGame;
    $("#notes").val(game.notes);
}

async function updateGame() {
    let game = validateInput(id);

    if (!game) {
        return;
    }

    await putGame(game);

    return game;
}

$("form").submit(async function (e) {
    e.preventDefault();

    let game = await updateGame();
    if (game) {
        GamesControl.setSelectedSeasonYear(game.seasonYear);
        GamesControl.setSelectedWeek(game.week);

        window.location.pathname = "Games/Index";
    }
});

loadPage();
