import { postData } from "../../data/repository.js";
import { validateInput } from "./shared.js";

async function createTeam() {
    let team = validateInput();

    if (!team) {
        return;
    }

    await postData("Teams", team);

    return team;
}

$("form").submit(async function (e) {
    e.preventDefault();

    let team = await createTeam();
    if (team) {
        window.location.pathname = "Teams/Index";
    }
});
