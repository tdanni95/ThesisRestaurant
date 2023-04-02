<script lang="ts">
    import SortTable from "$lib/components/SortTable.svelte";
    import type {
        CartCustomFood,
        CartFood,
        Order,
        OrderResponse,
    } from "$lib/types/classData";
    import type { PageData } from "./$types";
    import type { SortTableFormatters } from "$lib/types/sortTableFormatters";
    import Button from "$lib/components/Button.svelte";
    import Modal from "$lib/components/Modal.svelte";
    import { onMount } from "svelte";
    export let data: PageData;
    let orderData = data.orderData;

    orderData.forEach((od) => {
        od.guid = Math.floor(Math.random() * 10000);
    });
    let currentFoods: Array<CartFood> = [] as Array<CartFood>;
    let currentCustomFoods: Array<CartCustomFood> = [] as Array<CartCustomFood>;

    const dateFormatter = (orderResponse: OrderResponse) => {
        let date = new Date(orderResponse.created);
        return date.toLocaleString("hu-Hu", {
            year: "numeric",
            month: "2-digit",
            day: "2-digit",
            hour: "numeric",
            minute: "numeric",
            second: "numeric",
            hour12: false,
        });
    };

    const DateFormatter: SortTableFormatters<OrderResponse> = {
        name: "created",
        callBack: dateFormatter,
    };

    const foodIngredientsFormatter = (food: CartFood) => {
        return food.ingredients.map((i) => i.name).join(", ");
    };

    const IngredientFormatter: SortTableFormatters<CartFood> = {
        name: "ingredients",
        callBack: foodIngredientsFormatter,
    };

    const additionalIngredientFormatter = (food: CartFood) => {
        return food.additionalIngredients
            .map(
                (i) => `${i.name} - ${i.price * food.foodSizeMultiplier}Ft<br>`
            )
            .join(", ");
    };

    const AdditionalIngredientFormatter: SortTableFormatters<CartFood> = {
        name: "additionalIngredients",
        callBack: additionalIngredientFormatter,
    };

    const priceFormatter = (food: CartFood | CartCustomFood) => {
        return `${food.price} Ft`;
    };

    const PriceFormatter: SortTableFormatters<CartFood | CartCustomFood> = {
        name: "price",
        callBack: priceFormatter,
    };

    let modalTitle = {
        address: "",
        user: "",
        date: "",
    };
    let modalVisible = false;

    const displayOrder = (guid: number) => {
        if (!guid) return;
        let order = orderData.find((od) =>
            od.items.find((oi) => oi.id == guid)
        );
        if (!order) return;
        let currentOrder = order.items.find((i) => i.id == guid);

        if (!currentOrder) return;

        currentFoods = currentOrder.items.foods;

        currentFoods.forEach((element) => {
            element.guid = Math.random() * 10000;
        });

        currentCustomFoods = currentOrder.items.customFoods;

        modalTitle.user = order.user;
        modalTitle.address = currentOrder.address;
        modalTitle.date = dateFormatter(currentOrder);

        modalVisible = true;
    };

    let visibleOrder: number = orderData[0].guid;
    let currentOrder: Order = orderData[0];
</script>

{#if modalVisible}
    <Modal
        title={`Order details: ${modalTitle}`}
        close={() => (modalVisible = false)}
        bind:visible={modalVisible}
    >
        <div slot="header">
            <p class="text-2xl font-bold">Order details</p>
            <p class="text-xl">
                <span class="font-bold">Ordered by: </span>
                {modalTitle.user}
            </p>
            <p class="text-xl">
                <span class="font-bold">Address: </span>
                {modalTitle.address}
            </p>
            <p class="text-xl">
                <span class="font-bold">Ordered at: </span>{modalTitle.date}
            </p>
        </div>
        <div slot="body" class="overflow-auto max-h-96">
            <div class="w-full pb-10 px-5 md:px-10">
                <h1 class="font-bold text-3xl text-center text-gray-900">
                    Foods
                </h1>
                <SortTable
                    columns={[
                        "name",
                        "foodType",
                        "foodSize",
                        "quantity",
                        "ingredients",
                        "additionalIngredients",
                        "price",
                    ]}
                    idKey={"guid"}
                    needEditAndDelete={false}
                    formatters={[
                        IngredientFormatter,
                        AdditionalIngredientFormatter,
                        PriceFormatter,
                    ]}
                    ogArray={currentFoods}
                    deleteFunc={() => {}}
                    editFunc={() => {}}
                />
            </div>
            <div class="w-full pb-10 px-5 md:px-10">
                <h1 class="font-bold text-3xl text-center text-gray-900">
                    Custom Foods
                </h1>
                <SortTable
                    columns={[
                        "name",
                        "foodType",
                        "quantity",
                        "ingredients",
                        "price",
                    ]}
                    needEditAndDelete={false}
                    idKey={"guid"}
                    formatters={[IngredientFormatter, PriceFormatter]}
                    ogArray={currentCustomFoods}
                    deleteFunc={() => {}}
                    editFunc={() => {}}
                />
            </div>
        </div>
        <div slot="footer">
            <Button
                btnClass={"btn-primary"}
                on:click={() => (modalVisible = false)}
            >
                Close
            </Button>
        </div>
    </Modal>
{/if}

<div class="min-w-screen flex items-center justify-center px-5 py-5">
    <div class="bg-gray-200 text-gray-500 rounded-3xl shadow-xl w-11/12 py-10">
        <h1 class="font-bold text-3xl text-center text-gray-900">
            Previous orders of
            <select on:change={(e) => {
                const order = orderData.find((o) => o.guid == +e.currentTarget.value)
                console.log(orderData, e.currentTarget.value);
                if(!order) return
                currentOrder = order
            }}>
                {#each orderData as order}
                    <option value={order.guid}>{order.user}</option>
                {/each}
            </select>
        </h1>
        <div class="md:flex w-full">
            <div class="w-full pb-10 px-5 md:px-10">
                <!-- svelte-ignore missing-declaration -->
                <SortTable
                    columns={["address", "created"]}
                    idKey={"id"}
                    ogArray={currentOrder.items}
                    formatters={[DateFormatter]}
                    deleteFunc={() => {}}
                    editFunc={() => {}}
                >
                    <td
                        slot="actionButtons"
                        class="flex justify-center items-center text-center h-14"
                    >
                        <div class="w-6/12">
                            <Button
                                on:click={(e) => {
                                    const target = e.target;
                                    if (!target) return;
                                    //@ts-ignore
                                    const tr = target.closest("tr");
                                    if (!tr) return;
                                    const id = tr.dataset.id;
                                    displayOrder(+id);
                                }}>Show details</Button
                            >
                        </div>
                    </td>
                </SortTable>
            </div>
        </div>
    </div>
</div>
