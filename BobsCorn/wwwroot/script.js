const PRICE = 0.75

const countElement = document.getElementById("count");
const totalElement = document.getElementById("total");

async function buyCorn() {
    const url = "api/corn";

    const payload = {
        client: "John Doe"
    };

    try {
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Client-Name': 'foo'
            },
            body: JSON.stringify(payload)
        });

        if (response.status === 200) {
            count = Number(countElement.textContent) + 1;
            countElement.textContent = count;
            totalElement.textContent = (count * PRICE).toFixed(2);
        }

        if (response.status === 429) {
            alert('Too Many Requests');
        }

        if (response.status === 500) {
            alert('Error getting response');
        }

    } catch (error) {
        console.error("Error:", error);
    }
}