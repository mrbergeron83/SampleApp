import React, { ReactNode, createContext, useEffect, useState } from "react";
import { SampleApi } from "./__generated__/SampleApi";
import { appConfig } from "./appConfig";

const SampleApiContext = createContext<SampleApi<unknown> | undefined>(
  undefined
);

interface SampleClientProviderProps {
  children: ReactNode;
}

const SampleApiClientProvider = ({ children }: SampleClientProviderProps) => {
  const [client, setClient] = useState<SampleApi<unknown>>();

  useEffect(() => {
    // Initialize your API client here
    const api = new SampleApi({ baseUrl: appConfig.apiBaseUrl });
    setClient(api);
  }, []);

  return (
    <SampleApiContext.Provider value={client}>
      {children}
    </SampleApiContext.Provider>
  );
};

export { SampleApiClientProvider, SampleApiContext };
