async function getData(resource) {
    const url = `${api}/${resource}`;

    let data = null;

    await fetch(url, {
        method: "GET"
    }).then(async response => {
        data = await response.json();
    }).catch(async error => {
        console.log(await error);
    });

    return data;
}

async function postData(resource, data) {
    await postOrPutData(resource, "POST", data);
}

async function putData(resource, data) {
    await postOrPutData(resource, "PUT", data);
}

async function deleteData(resource) {
    const url = `${api}/${resource}`;

    await fetch(url, {
        method: "DELETE"
    }).then(async response => {
        console.log(await response);
    }).catch(async error => {
        console.log(await error);
    });
}

async function postOrPutData(resource, httpVerb, data) {
    const url = `${api}/${resource}`;

    await fetch(url, {
        method: httpVerb,
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    }).then(async response => {
        console.log(await response);
    }).catch(async error => {
        console.log(await error);
    });
}

export { getData, postData, putData, deleteData };
