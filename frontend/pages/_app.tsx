import "@mantine/core/styles.css";
import Head from "next/head";
import { AppShell, MantineProvider, Burger } from "@mantine/core";
import { useDisclosure } from '@mantine/hooks';
import { theme } from "../theme";
import Link from "next/link";

export default function App({ Component, pageProps }: any) {
  const [opened, { toggle }] = useDisclosure();

  return (
    <MantineProvider theme={theme}>
      <Head>
        <title>Mantine Template</title>
        <meta
          name="viewport"
          content="minimum-scale=1, initial-scale=1, width=device-width, user-scalable=no"
        />
        <link rel="shortcut icon" href="/favicon.svg" />
      </Head>
      <AppShell
        padding="md"
        header={{ height: 60 }}
        navbar={{ 
          width: 120, 
          breakpoint: 'sm',
          collapsed: { mobile: !opened },
        }}
      >
        <AppShell.Header>
        <Burger
          opened={opened}
          onClick={toggle}
          hiddenFrom="sm"
          size="sm"
        /> MataTeams </AppShell.Header>
        <AppShell.Navbar>
          <Link href="/">Dashboard</Link>
          <Link href="/projects">Projects</Link>
          <Link href="/profile">Profile</Link>
        </AppShell.Navbar>
        <AppShell.Main>
          <Component {...pageProps} />
        </AppShell.Main>
      </AppShell>
    </MantineProvider>
  );
}
