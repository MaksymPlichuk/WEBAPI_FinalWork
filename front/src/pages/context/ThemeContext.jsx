import { createContext, useState } from "react";

export const ThemeContext = createContext(null);

export const ThemeContextProvider = ({ children }) => {
    const [isDark, setIsDark] = useState(false);

    function setTheme() {
        setIsDark(!isDark);
    }

    return (
        <ThemeContext.Provider value={{ isDark, setTheme }}>
            {children}
        </ThemeContext.Provider>
    );
}