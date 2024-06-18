import { ProductType, CategoryWithProductTypes } from "../../../types/api/responseDataModels";

const jsonData = {
  "data": {
          "list": [
            {
              "id": "713e3cc4-08e3-4e92-8a8c-e06dd5ff3506",
              "name": "Кухня та їдальня",
              "slug": "kitchen_dining",
              "priority": null,
              "cardImageUri": "",
              "subCategories": [
                {
                  "id": "3548e224-05b8-46d3-99a1-d5ce919c5566",
                  "name": "Cтiльцi",
                  "slug": "kitchen_chairs",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "3fc4ddd7-67b9-4721-92fa-c8986b4c3652",
                  "name": "Зберігання",
                  "slug": "kitchen_storage",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "7ca4a088-199b-4090-ad3f-648804f0003b",
                  "name": "Столи",
                  "slug": "kitchen_tables",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                }
              ],
              "productTypes": null
            },
            {
              "id": "1123727f-346e-46f7-85eb-c6057e5ef7ad",
              "name": "Ванна",
              "slug": "bathroom",
              "priority": null,
              "cardImageUri": "",
              "subCategories": [
                {
                  "id": "bd16e8ef-38a5-48ee-ba6b-15f56c0633bf",
                  "name": "Аксесуари для ванної",
                  "slug": "bathroom_accessories",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "aae88973-4ea3-43b9-a278-3633c7d4f9c3",
                  "name": "Текстиль",
                  "slug": "bathroom_textiles",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                }
              ],
              "productTypes": null
            },
            {
              "id": "a2a1b18a-5157-4f16-a218-55ab8fb80e9d",
              "name": "Вітальня",
              "slug": "living_room",
              "priority": null,
              "cardImageUri": "",
              "subCategories": [
                {
                  "id": "9aad92e3-3c21-4805-9a83-1b2bd5026066",
                  "name": "Зберігання",
                  "slug": "livingroom_bookshelf",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "b4ab5b73-16ad-497e-ad4c-4c760e56a390",
                  "name": "Софи",
                  "slug": "livingroom_armchairs",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "b68a28ca-29d3-42f6-9c9e-c3efff7858a4",
                  "name": "Стільці",
                  "slug": "livingroom_chairs",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                },
                {
                  "id": "a422b096-e9d0-4992-98a0-3672ca731334",
                  "name": "Столи",
                  "slug": "livingroom_tables",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": null
                }
              ],
              "productTypes": null
            },
            {
              "id": "a324e488-fa3c-4ed1-8a35-be04dedbea5e",
              "name": "Офіс",
              "slug": "office",
              "priority": null,
              "cardImageUri": "",
              "subCategories": [
                {
                  "id": "29c7dbff-a722-4903-bee4-8391457c6343",
                  "name": "Зберігання",
                  "slug": "office_storage",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "4a9f28a5-f95a-42dc-bf56-cb96743c1d3f",
                      "categoryId": "29c7dbff-a722-4903-bee4-8391457c6343",
                      "name": "Книжкові шафи",
                      "slug": "office_bookcases",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "c0a32875-524f-4356-b986-87602cafae3b",
                      "categoryId": "29c7dbff-a722-4903-bee4-8391457c6343",
                      "name": "Полицi",
                      "slug": "office_shelves",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "5f367b6e-f589-4fb3-a5b7-e08e32720046",
                  "name": "Офісні меблі",
                  "slug": "office_furniture",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "860ff643-f720-48e2-b57d-03df48f5a44f",
                      "categoryId": "5f367b6e-f589-4fb3-a5b7-e08e32720046",
                      "name": "Офісні крісла",
                      "slug": "office_chairs",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "5c96a476-b07b-4543-98ed-d198bd26110a",
                      "categoryId": "5f367b6e-f589-4fb3-a5b7-e08e32720046",
                      "name": "Стіл комп'ютерний",
                      "slug": "office_computer_tables",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "6bd32b5c-02aa-4a4c-a0f5-b26bc40ace49",
                      "categoryId": "5f367b6e-f589-4fb3-a5b7-e08e32720046",
                      "name": "Столи з регулюванням висоти",
                      "slug": "office_tables_adjustable_height",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                }
              ],
              "productTypes": null
            },
            {
              "id": "56201d71-11d3-4fc4-a7ca-5db0148d66a0",
              "name": "Спальня",
              "slug": "bedroom",
              "priority": null,
              "cardImageUri": "",
              "subCategories": [
                {
                  "id": "664f53ac-275d-4e12-a78b-84c064d94d1a",
                  "name": "Ковдри",
                  "slug": "bedroom_rugs",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "dc3e0b16-a6ed-4f93-a171-97896b4eb07f",
                      "categoryId": "664f53ac-275d-4e12-a78b-84c064d94d1a",
                      "name": "Лляні ковдри",
                      "slug": "bedroom_lynen_blankets",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "0e89652e-a4b5-484a-a7f8-4ba474189083",
                      "categoryId": "664f53ac-275d-4e12-a78b-84c064d94d1a",
                      "name": "Натуральні ковдри",
                      "slug": "bedroom_natural_blankets",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "5d7bca5c-0fdc-4a75-99ae-df9167e9fdcc",
                      "categoryId": "664f53ac-275d-4e12-a78b-84c064d94d1a",
                      "name": "Шовкові ковдри",
                      "slug": "bedroom_silk_blankets",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "816c5f10-2001-4f69-9719-dbcaa07de4ac",
                  "name": "Ліжка",
                  "slug": "bedroom_beds",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "e8b5328d-62b5-43b5-a816-4b8fe7ad56e5",
                      "categoryId": "816c5f10-2001-4f69-9719-dbcaa07de4ac",
                      "name": "Розкладні ліжка",
                      "slug": "bedroom_folding_beds",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "c4e8726b-0fc6-492e-b405-7c4b6ef1f94c",
                      "categoryId": "816c5f10-2001-4f69-9719-dbcaa07de4ac",
                      "name": "Софи-ліжка",
                      "slug": "bedroom_sofa_beds",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "31f10ac0-db7b-4c88-9fb1-944706af628e",
                  "name": "Матраци",
                  "slug": "bedroom_mattresses",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "a9f0b8cd-d8c7-454d-adf3-e756a7e4c73c",
                      "categoryId": "31f10ac0-db7b-4c88-9fb1-944706af628e",
                      "name": "Безпружинні матраци",
                      "slug": "bedroom_springless_mattresses",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "f11b42cb-a726-4588-b95a-eb5a59d6de5c",
                      "categoryId": "31f10ac0-db7b-4c88-9fb1-944706af628e",
                      "name": "Пружинні матраци",
                      "slug": "bedroom_spring_mattresses",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "f933c253-f2a6-4773-acea-9076f7ec5162",
                      "categoryId": "31f10ac0-db7b-4c88-9fb1-944706af628e",
                      "name": "Пружинні матраци",
                      "slug": "bedroom_spring_mattresses",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "741bbe1c-22c3-4e99-8b6b-0c6272ffe114",
                  "name": "Меблі для спальні",
                  "slug": "bedroom_furniture",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "6dd3fc1b-73b1-4f87-b4d4-87a6fb3810e1",
                      "categoryId": "741bbe1c-22c3-4e99-8b6b-0c6272ffe114",
                      "name": "Дзеркала",
                      "slug": "bedroom_mirrors",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "65f6f228-bcf1-4038-8cc4-7dbc656765fb",
                      "categoryId": "741bbe1c-22c3-4e99-8b6b-0c6272ffe114",
                      "name": "Комоди",
                      "slug": "bedroom_dressers",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "e4ffe590-6495-412c-bc59-acccfa4ab16d",
                      "categoryId": "741bbe1c-22c3-4e99-8b6b-0c6272ffe114",
                      "name": "Пуфи",
                      "slug": "bedroom_poufs",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "00f266dd-c511-4c02-ab53-5a0e395941c3",
                  "name": "Подушки",
                  "slug": "bedroom_pillows",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "0851be62-02f6-4987-9096-75c1bfee036c",
                      "categoryId": "00f266dd-c511-4c02-ab53-5a0e395941c3",
                      "name": "Дитячі подушки",
                      "slug": "bedroom_children_pillows",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "3f214305-0b36-41a7-9383-f12dba2eefc7",
                      "categoryId": "00f266dd-c511-4c02-ab53-5a0e395941c3",
                      "name": "Натуральні подушки",
                      "slug": "bedroom_natural_pillows",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "bf582068-e0ff-4c63-821b-e916317201d6",
                      "categoryId": "00f266dd-c511-4c02-ab53-5a0e395941c3",
                      "name": "Спеціальні подушки",
                      "slug": "bedroom_special_pillows",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                },
                {
                  "id": "c49ca79a-eacc-4663-9529-9809dce3e0d6",
                  "name": "Постільна білизна",
                  "slug": "bedroom_linens",
                  "priority": null,
                  "cardImageUri": "",
                  "subCategories": null,
                  "productTypes": [
                    {
                      "id": "553effcb-6ab7-4af5-80e3-934e3a1a6965",
                      "categoryId": "c49ca79a-eacc-4663-9529-9809dce3e0d6",
                      "name": "Наволочки",
                      "slug": "bedroom_pillowcases",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "21b85995-9dab-4e5d-914e-4807747d4687",
                      "categoryId": "c49ca79a-eacc-4663-9529-9809dce3e0d6",
                      "name": "Покривала",
                      "slug": "bedroom_covers",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    },
                    {
                      "id": "aba2eca5-082f-4f8e-bdbb-423f449c8f60",
                      "categoryId": "c49ca79a-eacc-4663-9529-9809dce3e0d6",
                      "name": "Простирадла",
                      "slug": "bedroom_sheets",
                      "priority": null,
                      "cardImageUri": "",
                      "thumbnailImageUri": ""
                    }
                  ]
                }
              ],
              "productTypes": null
            }
          ]
        }
}

function mapCategory(category: any): CategoryWithProductTypes {
  const mappedCategory: CategoryWithProductTypes = {
    id: category.id,
    name: category.name,
    slug: category.slug,
    priority: category.priority,
    cardImageUri: category.cardImageUri,
    thumbnailImageUri: category.cardImageUri, // Assuming thumbnail is the same as cardImageUri if not provided
    subCategoryList: category.subCategories ? category.subCategories.map(mapCategory) : null,
    productTypes: category.productTypes ? category.productTypes.map((pt: any) => ({
      id: pt.id,
      categoryId: pt.categoryId,
      name: pt.name,
      slug: pt.slug,
      priority: pt.priority,
      cardImageUri: pt.cardImageUri,
      thumbnailImageUri: pt.thumbnailImageUri || pt.cardImageUri // Assuming the same fallback
    })) : null
  };
  return mappedCategory;
}

const mappedData = jsonData.data.list.map(mapCategory);

export const dummyData = mappedData;