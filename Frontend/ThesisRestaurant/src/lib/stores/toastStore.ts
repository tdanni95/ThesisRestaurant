import { writable } from "svelte/store";

interface Toast {
    id: number;
    message: string;
    type: "success" | "error" | "warning" | "info";
    timeout?: number;
}

function createToastsStore() {
	const { subscribe, update } = writable<Toast[]>([]);

	function addToast({
		type,
		message,
		id,
		timeout = 2000
	}: {
		type: Toast['type'];
		message: string;
		id: number;
		timeout?: number;
	}) {
		update((toasts) => [{ type, message, id }, ...toasts]);
		if (timeout) {
			setTimeout(() => {
				removeToast(id);
			}, timeout);
		}
	}

	function removeToast(id: number) {
		update((toasts) => toasts.filter((t) => t.id !== id));
	}
	return {
		subscribe,
		info: (message: string, timeout?: number) =>
			addToast({
				type: 'info',
				message,
				timeout,
				id: Math.floor(Math.random() * 1000000)
			}),
		warning: (message: string, timeout?: number) =>
			addToast({
				type: 'warning',
				message,
				timeout,
				id: Math.floor(Math.random() * 1000000)
			}),
		error: (message: string, timeout?: number) =>
			addToast({
				type: 'error',
				message,
				timeout,
				id: Math.floor(Math.random() * 1000000)
			}),
		success: (message: string, timeout?: number) =>
			addToast({
				type: 'success',
				message,
				timeout,
				id: Math.floor(Math.random() * 1000000)
			}),
		remove: (id: number) => removeToast(id)
	};
}

export default createToastsStore();