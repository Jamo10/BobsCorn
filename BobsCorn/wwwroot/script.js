const PRICE = 0.75

const countElement = document.getElementById("count");
const totalElement = document.getElementById("total");
const clientElement = document.getElementById("clientSelect");

async function buyCorn() {
    const url = "api/corn";

    if (!clientElement.value) {
        alert('Select a client')
        return;
    }

    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Client-Name': clientElement.value
            }
        });

        if (response.status === 200) {
            var data = await response.json();

            countElement.textContent = data.totalClient;
            totalElement.textContent = (data.totalClient * PRICE).toFixed(2);
        }

        if (response.status === 429) {
            alert('Too Many Requests.');
        }

        if (response.status === 409) {
            var data = response.json();
            alert(data.message)
        }

        if (response.status === 500) {
            alert('Error getting response.');
        }

    } catch (error) {
        console.error("Error:", error);
    }
}

clientElement.addEventListener("change", async (event) => {
    const url = "api/clientcorn?clientName=" + event.target.value;

    try {
        const response = await fetch(url, {
            method: "GET",
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (response.status === 200) {
            var data = await response.json();
            countElement.textContent = data.totalClient;
            totalElement.textContent = (data.totalClient * PRICE).toFixed(2);
        }

        if (response.status === 500) {
            alert('Error getting response.');
        }

    } catch (error) {
        console.error("Error:", error);
    }
});