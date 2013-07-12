--Ｎｏ．５７ トライヘッド・ダスト・ドラゴン
function c53244294.initial_effect(c)
  --xyz summon
	aux.AddXyzProcedure(c,aux.XyzFilterFunction(c,4),3)
	c:EnableReviveLimit()
	--atk def
	local e1=Effect.CreateEffect(c)
	e1:SetDescription(aux.Stringid(53244294,0))
	e1:SetCategory(CATEGORY_ATKCHANGE)
	e1:SetType(EFFECT_TYPE_SINGLE+EFFECT_TYPE_TRIGGER_O)
	e1:SetProperty(EFFECT_FLAG_CARD_TARGET)
	e1:SetCode(EVENT_SPSUMMON_SUCCESS)
	e1:SetTarget(c53244294.target)
	e1:SetOperation(c53244294.operation)
	c:RegisterEffect(e1)
	--dis field
	local e2=Effect.CreateEffect(c)
	e2:SetType(EFFECT_TYPE_IGNITION)
	e2:SetRange(LOCATION_MZONE)
	e2:SetCondition(c53244294.discon)
	e2:SetCost(c53244294.discost)
	e2:SetTarget(c53244294.distg)
	e2:SetOperation(c53244294.disop)
	c:RegisterEffect(e2)
end
function c53244294.target(e,tp,eg,ep,ev,re,r,rp,chk,chkc)
	if chkc then return chkc:IsControler(1-tp) and chkc:IsLocation(LOCATION_MZONE) and chkc:IsFaceup() end
	if chk==0 then return true end
	Duel.Hint(HINT_SELECTMSG,tp,HINTMSG_FACEUP)
	Duel.SelectTarget(tp,Card.IsFaceup,tp,0,LOCATION_MZONE,1,1,nil)
end
function c53244294.operation(e)
	local tc=Duel.GetFirstTarget()
	local c=e:GetHandler()
	if c:IsFaceup() and c:IsRelateToEffect(e) and tc and tc:IsFaceup() and tc:IsRelateToEffect(e) then
		local e1=Effect.CreateEffect(c)
		e1:SetType(EFFECT_TYPE_SINGLE)
		e1:SetCode(EFFECT_UPDATE_ATTACK)
		e1:SetValue(tc:GetAttack())
		e1:SetReset(RESET_EVENT+0x1ff0000)
		c:RegisterEffect(e1)
	end
end

function c53244294.discon(e,tp)
	 return  Duel.GetFieldGroupCount(tp,LOCATION_ONFIELD,0,nil)<Duel.GetFieldGroupCount(tp,0,LOCATION_ONFIELD,nil)
end

function c53244294.discost(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return e:GetHandler():CheckRemoveOverlayCard(tp,1,REASON_COST) end
	e:GetHandler():RemoveOverlayCard(tp,1,1,REASON_COST)
end

function c53244294.distg(e,tp,eg,ep,ev,re,r,rp,chk)
	if chk==0 then return
	Duel.GetLocationCount(tp,LOCATION_SZONE)+Duel.GetLocationCount(1-tp,LOCATION_SZONE)+
	Duel.GetLocationCount(tp,LOCATION_MZONE)+Duel.GetLocationCount(1-tp,LOCATION_MZONE)
	>1
	end
	local dis=Duel.SelectDisableField(tp,1,LOCATION_ONFIELD,LOCATION_ONFIELD,0)
	e:SetLabel(dis)
end

function c53244294.disop(e,tp)
	local c=e:GetHandler()
	local dis=e:GetLabel()
	--disable field
	local e1=Effect.CreateEffect(c)
	e1:SetType(EFFECT_TYPE_FIELD)
	e1:SetRange(LOCATION_MZONE)
	e1:SetLabel(dis)
	e1:SetCode(EFFECT_DISABLE_FIELD)
	e1:SetOperation(c53244294.op)
	e1:SetReset(RESET_EVENT+0x1ff0000)
	c:RegisterEffect(e1)
end
function c53244294.op(e,tp)
	return e:GetLabel()
end


