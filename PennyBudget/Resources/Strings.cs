using System.Resources;

namespace PennyBudget.Resources;

public static class Strings
{
    private static readonly ResourceManager Rm =
        new("PennyBudget.Resources.Strings", typeof(Strings).Assembly);

    private static string Get(string key) => Rm.GetString(key) ?? key;

    public static string TabDashboard  => Get("tab_dashboard");
    public static string TabRecords    => Get("tab_records");
    public static string TabCategories => Get("tab_categories");
    public static string TabSettings   => Get("tab_settings");

    public static string DashboardIncome   => Get("dashboard_income");
    public static string DashboardExpenses => Get("dashboard_expenses");
    public static string DashboardBalance  => Get("dashboard_balance");

    public static string ColDate        => Get("col_date");
    public static string ColCategory    => Get("col_category");
    public static string ColDescription => Get("col_description");
    public static string ColAmount      => Get("col_amount");
    public static string ColAccount     => Get("col_account");
    public static string ColCurrency    => Get("col_currency");
    public static string ColRate        => Get("col_rate");
    public static string ColPln         => Get("col_pln");
    public static string ColName        => Get("col_name");
    public static string ColColor       => Get("col_color");
    public static string ColIncome      => Get("col_income");
    public static string ColSystem      => Get("col_system");

    public static string BtnAdd    => Get("btn_add");
    public static string BtnEdit   => Get("btn_edit");
    public static string BtnDelete => Get("btn_delete");
    public static string BtnSave   => Get("btn_save");
    public static string BtnCancel => Get("btn_cancel");

    public static string SearchPlaceholder => Get("search_placeholder");

    public static string LblCategoryName   => Get("lbl_category_name");
    public static string LblCategoryNamePh => Get("lbl_category_name_ph");
    public static string LblIsIncome       => Get("lbl_is_income");
    public static string LblCategoryColor  => Get("lbl_category_color");
    public static string LblDescription    => Get("lbl_description");
    public static string LblAccount        => Get("lbl_account");
    public static string LblAmount         => Get("lbl_amount");
    public static string LblCurrency       => Get("lbl_currency");
    public static string LblRate           => Get("lbl_rate");
    public static string LblDate           => Get("lbl_date");

    public static string LblSettingsLanguage => Get("lbl_settings_language");
    public static string LblSettingsRestart  => Get("lbl_settings_restart");

    public static string TitleAddCategory  => Get("title_add_category");
    public static string TitleEditCategory => Get("title_edit_category");
    public static string TitleAddRecord    => Get("title_add_record");
    public static string TitleEditRecord   => Get("title_edit_record");
}
