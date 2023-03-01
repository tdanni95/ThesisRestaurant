<script lang="ts">
    const doRegister = async () => {
        const url = "https://localhost:7068/authentication/register";
        const response = await fetch(url, {
            method: "POST",
            body: JSON.stringify({ firstName, lastName, email, password }),
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json",
            },
        });
        const res = await response.json();
        console.log(res);
    };
    let firstName = "";
    let lastName = "";
    let email = "";
    let password = "";
</script>

<form on:submit|preventDefault={doRegister}>
    <input type="text" bind:value={firstName} placeholder="firstName" />
    <br />
    <input type="text" bind:value={lastName} placeholder="lastName" />
    <br />
    <input type="text" bind:value={email} placeholder="email" />
    <br />
    <input type="text" bind:value={password} placeholder="password" />
    <br />
    <button>Register</button>
</form>
