import { Container, getContainer } from "@cloudflare/containers";

export class MoonbeamContainer extends Container {
  defaultPort = 5000;
  sleepAfter = "10m";
}

interface Env {
  MOONBEAM: DurableObjectNamespace<MoonbeamContainer>;
}

export default {
  async fetch(request: Request, env: Env): Promise<Response> {
    return getContainer(env.MOONBEAM).fetch(request);
  },
} satisfies ExportedHandler<Env>;
